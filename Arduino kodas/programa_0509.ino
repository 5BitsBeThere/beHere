// Paskutinis modifikavimas: 05_02
/* TO DO:
     Pridėsim Wifi biblioteka (kazkada gal)
     metodas arNuskaito() su iterpimu patvarkyt
     padaryt paskutiniu zmoniu atvaizdavima
     padaryt kad loggintu informacija i SD kortele

    **padaryt pino nustatyma (KAI BUS LAIKO)
    **padaryt klaviatura wifi duomenu ivedimui (GAAAL, KAI BUS LAIKO) programa
      klaviaturos ant ekrano butu suprogramuota, kad paprasciau butu
    **padaryt kad tie power save ir log size nustatymai ka nors darytu(IGI NE DB)

     PCB pasidaryt, nes atsibodo ant to breadbordo
     ♥♥♥♥♥SUSIRINKT PAGERT♥♥♥♥♥
*/

/* DONE:
   rtc panaudojimas
   laiko/datos rodymas
   temperaturos rodymas
   laiko nustatymas
   datos nustatymas
   pin ivedimas
   nustatymu issaugojimas eeprom atmintyje (dar ne viskas)
   sukurt darbuotoju struktura ( vardas, pavarde, ID )
   korteles nuskaitymas ir veiksmai ja nuskaicius
   keliamieji metai veikia
   duomenu nuskaitymas is SD korteles ir ju irasymas i darbuotoju struktura
   saraso kitamuju aprasymas paskutiniu darbuotoju atvaizdavimui
*/
#include<SPI.h>
#include<SD.h>
#include <Wire.h>
#include <RtcDS3231.h>      // realaus laiko laikrodzio modulio biblioteka
#include <Nextion.h>        // nextion ekrano valdymo biblioteka
#include <EEPROM.h>         // rasymo i eeprom biblioteka
#include <Wiegand.h>

#define countof(a) (sizeof(a) / sizeof(a[0]))

RtcDS3231<TwoWire> Laikrodis(Wire); // google > DS3231 > rasit kaip atrodo jis
WIEGAND skaitytuvas;                // wiegand rfid korteliu skaitytuvas
File failas;                        // failo objektas darbui su sd kortele

// darbuotoju struktura
struct darbuotojas {
  char vardPav[30];
  unsigned long id;
};

//struktura darbuotoju vaizdavimui ekrane
struct darb_vaizdavimas
{
  char vardPav[30];
  char data[17];
};


// darbuotoju masyvas
// irasomas is SD korteles
// uzpildomas is failo
darbuotojas A[50];
int darb_count = 0;     // darbuotoju skaicius

// paskutiniai nusiskenave darbuotojai
// naudosim atvaizdavimui ekrane
darb_vaizdavimas last_darb[30] = {
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"},
  {"Justas", "12/12/12 15:14"}
  };

int8_t counteris = 0;              //kiek darbuotoju yra masyve
uint8_t sarasoPuslapis = 1;        //saraso esamas puslapis

// puslapiu numeriai
char esamas_puslapis = 0;   // naudojam char kai nereikia didesnes reiksmes nei 255 (8bitai čia)
char buves_puslapis = 0;    // taupom ram kitaip tariant
int8_t kur_eit = -1;        //del pin kodo kad nepasimest kur eit

// pin nustatymai
char pin_tikras[4] = {'0', '0', '0', '0'};
char pin_vedamas[5];
int8_t indx = 0;

// laiko atnaujinimo kintamieji
int16_t interval = 1000;          // naudosim kad laika kas minute atnaujintu
unsigned long buves_laikas = 0;

int8_t buves_min = -1;
int8_t buves_diena = -1;

int16_t intervalas1 = 2000; // po 2s gristi is pranesimo puslapio (page5)
unsigned long buves_laikas1 = 0;

// o naujinsim kas minute kad neuzkimštų serial protokolo
// tada beveik visada neveiktu mygtukų paspaudimai
// nes nebutu kada ekranui issiust komandą i MCU

//laiko kintamieji
char valandos = 0;
char minutes = 0;

//datos kintamieji
int metai_nust = 2001;
char menesis_nust = 1;
char diena_nust = 1;

// nustatymu kintamieji
uint32_t log_size = 1;
int32_t power_save = 1;

// 0 puslapio kintamieji
// (pirmas argumentas - puslapio numeris)
// (antras - objekto ID nextion editoryje (čia programa tai grafinei sasajai kurt))
// (trecias - to objekto pavadinimas)
NexPage page0 = NexPage(0, 0, "page0");
NexButton nustatymai = NexButton(0, 1, "nustatymai");
NexButton laikas = NexButton(0, 2, "laikas");
NexButton data = NexButton(0, 3, "data");
NexButton sarasas = NexButton(0, 4, "sarasas");
NexText dat = NexText(0, 5, "dat");
NexText temp = NexText(0, 6, "temp");
NexText versija = NexText(0, 7, "versija");
NexNumber minut = NexNumber(0, 11, "minut");
NexNumber val = NexNumber(0, 9, "val");

// 1 puslapio kintamieji
NexPage page1 = NexPage(1, 0, "page1");
NexButton namai1 = NexButton(1, 1, "namai");
NexText pin = NexText(1, 12, "pin");
NexButton b1 = NexButton(1, 2, "b1");
NexButton b2 = NexButton(1, 3, "b2");
NexButton b3 = NexButton(1, 4, "b3");
NexButton b4 = NexButton(1, 5, "b4");
NexButton b5 = NexButton(1, 6, "b5");
NexButton b6 = NexButton(1, 7, "b6");
NexButton b7 = NexButton(1, 8, "b7");
NexButton b8 = NexButton(1, 9, "b8");
NexButton b9 = NexButton(1, 10, "b9");
NexButton b10 = NexButton(1, 11, "b10");
NexButton trinti = NexButton(1, 13, "trinti");
NexButton patvirtinti = NexButton(1, 14, "patvirtinti");

// 2 puslapio kintamieji
NexPage page2 = NexPage(2, 0, "page2");
NexButton namai2 = NexButton(2, 1, "namai");
NexButton plusMetai = NexButton(2, 2, "b1");
NexButton minusMetai = NexButton(2, 3, "b2");
NexButton plusMenesis = NexButton(2, 4, "b3");
NexButton minusMenesis = NexButton(2, 5, "b4");
NexButton plusDiena = NexButton(2, 6, "b5");
NexButton minusDiena = NexButton(2, 7, "b6");
NexNumber metai = NexNumber(2, 10, "metai");
NexNumber menesis = NexNumber(2, 11, "menesis");
NexNumber diena = NexNumber(2, 12, "diena");
NexButton patvirtinti_data = NexButton(2, 13, "patvirtinti");

// 3 puslapio kintamieji
NexPage page3 = NexPage(3, 0, "page3");
NexButton namai3 = NexButton(3, 1, "namai");
NexButton plusVal = NexButton(3, 2, "b3");
NexButton minusVal = NexButton(3, 3, "b4");
NexButton plusMin = NexButton(3, 4, "b5");
NexButton minusMin = NexButton(3, 5, "b6");
NexNumber n1 = NexNumber(3, 6, "n1");
NexNumber n2 = NexNumber(3, 8, "n2");
NexButton patvirtinti_laika(3, 9, "patvirtinti");

// 4 puslapio kintamieji
NexPage page4 = NexPage(4, 0, "page4");
NexButton namai4 = NexButton(4, 1, "namai");
NexNumber sk1 = NexNumber(4, 3, "sk1");
NexNumber sk2 = NexNumber(4, 6, "sk2");
NexNumber sk3 = NexNumber(4, 7, "sk3");
NexSlider failuKiekis = NexSlider(4, 4, "failuKiekis");
NexSlider saveLaikas = NexSlider(4, 5, "saveLaikas");
NexButton patvirtinti_nust = NexButton(4, 8, "patvirtinti");

// 5 puslapio kintamieji
NexPage page5 = NexPage(5, 0, "page5");
NexButton namai5 = NexButton(5, 1, "namai");
NexText pranesimas = NexText(5, 2, "pranesimas");

// 6 puslapio kintamieji
NexPage page6 = NexPage(6, 0, "page6");
NexButton namai6 = NexButton(6, 1, "namai");
NexButton upButton = NexButton(6, 22, "upButton");     // aukstina psl skaiciu
NexButton downButton = NexButton(6, 23, "downButton"); // zemina psl skaiciu
NexNumber psl = NexNumber(6, 24, "psl");// nurodo kuriame saraso puslapyje
NexText t0 = NexText(6, 2, "t0");       // nuo cia prasideda darbuotojo varpav atvaizdavimas
NexText t1 = NexText(6, 3, "t1");
NexText t2 = NexText(6, 4, "t2");
NexText t3 = NexText(6, 5, "t3");
NexText t4 = NexText(6, 6, "t4");
NexText t5 = NexText(6, 7, "t5");
NexText t6 = NexText(6, 8, "t6");
NexText t7 = NexText(6, 9, "t7");
NexText t8 = NexText(6, 10, "t8");
NexText t9 = NexText(6, 11, "t9");
NexText t10 = NexText(6, 12, "t10");      // nuo cia prasideda datos atvaizdavimas
NexText t11 = NexText(6, 13, "t11");
NexText t12 = NexText(6, 14, "t12");
NexText t13 = NexText(6, 15, "t13");
NexText t14 = NexText(6, 16, "t14");
NexText t15 = NexText(6, 17, "t15");
NexText t16 = NexText(6, 18, "t16");
NexText t17 = NexText(6, 19, "t17");
NexText t18 = NexText(6, 20, "t18");
NexText t19 = NexText(6, 21, "t19");


//--------------------- dar pridėsim daugiau puslapių-------------------------------
//                      nes reiks manau keleto
//----------------------------------------------------------------------------------

// masyvas skirtas saugoti objektus iš kurių gaus kažkokias lietimo komandas
// pagrinde mygtukai, bet gali būt bet kas
NexTouch *nex_listen_list[] =
{
  // 0 puslapio mygtukai
  &nustatymai,
  &laikas,
  &data,
  &sarasas,
  // 1 puslapio mygtukai
  &namai1,
  &b1,
  &b2,
  &b3,
  &b4,
  &b5,
  &b6,
  &b7,
  &b8,
  &b9,
  &b10,
  &trinti,
  &patvirtinti,
  // 2 puslapio mygtukai
  &namai2,
  &plusMetai,
  &minusMetai,
  &plusMenesis,
  &minusMenesis,
  &plusDiena,
  &minusDiena,
  &patvirtinti_data,
  // 3 puslapio mygtukai
  &namai3,
  &plusVal,
  &minusVal,
  &plusMin,
  &minusMin,
  &patvirtinti_laika,
  // 4 puslapio mygtukai
  &namai4,
  &patvirtinti_nust,
  // 5 puslapio mygtukai
  &namai5,
  // 6 puslapio mygtukai
  &namai6,
  &upButton,
  &downButton,
  NULL
};



// Paspaudimų metodai (rašom ką norima vykdyti paspaudus mygtuką).
// Visai kaip visual studio, tik čia patys tuo metodus susirašom, niekas paspaudus du kart nesugeneruoja :D :D
// 0 puslapio
void nustatymaiPopCallback(void *ptr)
{
  page1.show();
  esamas_puslapis = 1;   // paspaudus atsidursim 4 puslapyje
  kur_eit = 4;
  buves_puslapis = 0;    // atejom is pradinio (0 puslapio)
}
void laikasPopCallback(void *ptr)
{
  page1.show();
  esamas_puslapis = 1;
  kur_eit = 3;
  buves_puslapis = 0;
}
void sarasasPopCallback(void *ptr)
{
  page6.show();
  esamas_puslapis = 6;
  buves_puslapis = 0;

  //Paspaudus saraso knopke, isspausdina duomenis apie pirmus 10 darbuotoju

  t0.setText(last_darb[0].vardPav);
  t10.setText(last_darb[0].data);

  t1.setText(last_darb[1].vardPav);
  t11.setText(last_darb[1].data);

  t2.setText(last_darb[2].vardPav);
  t12.setText(last_darb[2].data);

  t3.setText(last_darb[3].vardPav);
  t13.setText(last_darb[3].data);

  t4.setText(last_darb[4].vardPav);
  t14.setText(last_darb[4].data);

  t5.setText(last_darb[5].vardPav);
  t15.setText(last_darb[5].data);

  t6.setText(last_darb[6].vardPav);
  t16.setText(last_darb[6].data);

  t7.setText(last_darb[7].vardPav);
  t17.setText(last_darb[7].data);

  t8.setText(last_darb[8].vardPav);
  t18.setText(last_darb[8].data);

  t9.setText(last_darb[9].vardPav);
  t19.setText(last_darb[9].data);

  //ISSPAUSDINO


}
void dataPopCallback(void *ptr)
{
  page1.show();
  esamas_puslapis = 1;
  kur_eit = 2;
  buves_puslapis = 0;
}
// 1 puslapio

void namai1PopCallback(void *ptr)
{
  page0.show();
  esamas_puslapis = 0;
  buves_puslapis = 1;
  memset(pin_vedamas, 0, sizeof(pin_vedamas));
  indx = 0;
}
void b1PopCallback(void *ptr)
{
  if (indx != 4)
  {
    pin_vedamas[indx++] = '1';
    pin.setText(pin_vedamas);
  }
}
void b2PopCallback(void *ptr)
{
  if (indx != 4)
  {
    pin_vedamas[indx++] = '2';
    pin.setText(pin_vedamas);
  }
}
void b3PopCallback(void *ptr)
{
  if (indx != 4)
  {
    pin_vedamas[indx++] = '3';
    pin.setText(pin_vedamas);
  }
}
void b4PopCallback(void *ptr)
{
  if (indx != 4)
  {
    pin_vedamas[indx++] = '4';
    pin.setText(pin_vedamas);
  }
}
void b5PopCallback(void *ptr)
{
  if (indx != 4)
  {
    pin_vedamas[indx++] = '5';
    pin.setText(pin_vedamas);
  }
}
void b6PopCallback(void *ptr)
{
  if (indx != 4)
  {
    pin_vedamas[indx++] = '6';
    pin.setText(pin_vedamas);
  }
}
void b7PopCallback(void *ptr)
{
  if (indx != 4)
  {
    pin_vedamas[indx++] = '7';
    pin.setText(pin_vedamas);
  }
}
void b8PopCallback(void *ptr)
{
  if (indx != 4)
  {
    pin_vedamas[indx++] = '8';
    pin.setText(pin_vedamas);
  }
}
void b9PopCallback(void *ptr)
{
  if (indx != 4)
  {
    pin_vedamas[indx++] = '9';
    pin.setText(pin_vedamas);
  }
}
void b10PopCallback(void *ptr)
{
  if (indx != 4)
  {
    pin_vedamas[indx++] = '0';
    pin.setText(pin_vedamas);
  }
}
void trintiPopCallback(void *ptr)
{
  memset(pin_vedamas, 0, sizeof(pin_vedamas));
  pin.setText(pin_vedamas);
  indx = 0;

}
void patvirtintiPopCallback(void *ptr)
{
  for (int i = 0; i < 4; i++)
  {
    if (pin_tikras[i] != pin_vedamas[i])
    {
      memset(pin_vedamas, 0, sizeof(pin_vedamas));
      pin.setText(pin_vedamas);
      indx = 0;
      return;
    }
  }

  switch (kur_eit)
  {
    case 2 :
      esamas_puslapis = 2;
      buves_puslapis = 1;
      page2.show();
      break;
    case 3 :
      esamas_puslapis = 3;
      buves_puslapis = 1;
      page3.show();
      break;
    case 4 :
      esamas_puslapis = 4;
      buves_puslapis = 1;
      page4.show();

      sk2.setValue(log_size);
      failuKiekis.setValue(log_size);

      sk3.setValue(power_save);
      saveLaikas.setValue(power_save);
      break;
  }
  memset(pin_vedamas, 0, sizeof(pin_vedamas));
  pin.setText(pin_vedamas);
  indx = 0;
}
// 2 puslapio
void namai2PopCallback(void *ptr)
{
  metai_nust = 2001;
  menesis_nust = 1;
  diena_nust = 1;
  page0.show();
  esamas_puslapis = 0;
  buves_puslapis = 2;
}
void plusMetaiPopCallback(void *ptr)
{
  menesis_nust = 1;
  if (metai_nust < 2099)
    metai_nust++;
  else metai_nust = 2001;
  metai.setValue(metai_nust);
  menesis.setValue(menesis_nust); //pakeitus metus nunulina menesi
}
void minusMetaiPopCallback(void *ptr)
{
  menesis_nust = 1;
  if (metai_nust > 2001)
    metai_nust--;
  else metai_nust = 2099;
  metai.setValue(metai_nust);
  menesis.setValue(menesis_nust); //pakeitus metus nunulina menesi
}
void plusMenesisPopCallback(void *ptr)
{
  diena_nust = 1;
  if (menesis_nust < 12)
    menesis_nust++;
  else menesis_nust = 1;
  menesis.setValue(menesis_nust);
  diena.setValue(diena_nust);
}
void minusMenesisPopCallback(void *ptr)
{
  diena_nust = 1;
  if (menesis_nust > 1)
    menesis_nust--;
  else menesis_nust = 12;
  menesis.setValue(menesis_nust);
  diena.setValue(diena_nust);
}
void plusDienaPopCallback(void *ptr)
{
  if (menesis_nust == 1 || menesis_nust == 3 || menesis_nust == 5 || menesis_nust == 7 ||
      menesis_nust == 8 || menesis_nust == 10 || menesis_nust == 12)
  {
    if (diena_nust < 31)
      diena_nust++;
    else diena_nust = 1;
  }
  else if (menesis_nust == 4 || menesis_nust == 6 || menesis_nust == 9 || menesis_nust == 11)
  {
    if (diena_nust < 30)
      diena_nust++;
    else diena_nust = 1;
  }
  else if (menesis_nust == 2)
  {
    if (metai_nust % 400 == 0 || (metai_nust % 100 != 0 && metai_nust % 4 == 0)) //keliamieji
    {
      if (diena_nust < 29)
        diena_nust++;
      else diena_nust = 1;
    }
    else
    {
      if (diena_nust < 28)
        diena_nust++;
      else diena_nust = 1;
    }
  }
  diena.setValue(diena_nust);
}
void minusDienaPopCallback(void *ptr)
{

  if (menesis_nust == 1 || menesis_nust == 3 || menesis_nust == 5 || menesis_nust == 7 ||
      menesis_nust == 8 || menesis_nust == 10 || menesis_nust == 12)
  {
    if (diena_nust > 1)
      diena_nust--;
    else diena_nust = 31;
  }
  else if (menesis_nust == 4 || menesis_nust == 6 || menesis_nust == 9 || menesis_nust == 11)
  {
    if (diena_nust > 1)
      diena_nust--;
    else diena_nust = 30;
  }
  else if (menesis_nust == 2)
  {

    if (metai_nust % 400 == 0 || (metai_nust % 100 != 0 && metai_nust % 4 == 0)) //keliamieji
    {
      if (diena_nust > 1)
        diena_nust--;
      else diena_nust = 29;
    }
    else
    {
      if (diena_nust < 1)
        diena_nust--;
      else diena_nust = 28;
    }
  }

  diena.setValue(diena_nust);
}
void patvirtinti_dataPopCallback(void *ptr)
{
  RtcDateTime dabar = Laikrodis.GetDateTime();
  RtcDateTime nustatyti = RtcDateTime(metai_nust, menesis_nust, diena_nust, dabar.Hour(), dabar.Minute(), dabar.Second());
  Laikrodis.SetDateTime(nustatyti);

  metai_nust = 2001;
  menesis_nust = 1;
  diena_nust = 1;
  page0.show();
  esamas_puslapis = 0;
  buves_puslapis = 2;
}
// 3 puslapio
void namai3PopCallback(void *ptr)
{
  minutes = 0;
  valandos = 0;
  page0.show();
  esamas_puslapis = 0;
  buves_puslapis = 3;
}
void plusValPopCallback(void *ptr)
{
  if (valandos != 23)
    valandos++;
  else valandos = 0;
  n1.setValue(valandos);
}
void minusValPopCallback(void *ptr)
{
  if (valandos != 0)
    valandos--;
  else valandos = 23;
  n1.setValue(valandos);
}
void plusMinPopCallback(void *ptr)
{
  if (minutes != 59)
    minutes++;
  else minutes = 0;
  n2.setValue(minutes);
}
void minusMinPopCallback(void *ptr)
{
  if (minutes != 0)
    minutes--;
  else minutes = 59;
  n2.setValue(minutes);
}
void patvirtinti_laikaPopCallBack(void *ptr) {

  RtcDateTime dabar = Laikrodis.GetDateTime();
  RtcDateTime nustatyti = RtcDateTime(dabar.Year(), dabar.Month(), dabar.Day(), valandos, minutes, 0);
  Laikrodis.SetDateTime(nustatyti);

  minutes = 0;
  valandos = 0;
  page0.show();
  esamas_puslapis = 0;
  buves_puslapis = 3;
}

// 4 puslapio
void namai4PopCallback(void *ptr)
{
  page0.show();
  esamas_puslapis = 0;
  buves_puslapis = 4;
}
void patvirtinti_nustPopCallback(void *ptr)
{
  uint32_t reiksme;
  sk2.getValue(&reiksme);
  if (reiksme != log_size)
  {
    log_size = reiksme;
    EEPROM.write(0, log_size);
    EEPROM.commit();
  }
  sk3.getValue(&reiksme);
  if (reiksme != power_save)
  {
    power_save = reiksme;
    EEPROM.write(1, power_save);
    EEPROM.commit();
  }
  page0.show();
  esamas_puslapis = 0;
  buves_puslapis = 4;
}
// 5 puslapio
void namai5PopCallback(void *ptr)
{
  page0.show();
  esamas_puslapis = 0;
  buves_puslapis = 5;
}
// 6 puslapio
void namai6PopCallback(void *ptr)
{
  page0.show();
  esamas_puslapis = 0;
  buves_puslapis = 6;
}

void upButtonPopCallback(void *ptr)
{
  if (sarasoPuslapis != 3)
  {
    sarasoPuslapis++;

    //Atvaizduojami kiti 10 darbuotoju
    t0.setText(last_darb[0+(10*(sarasoPuslapis-1))].vardPav);
    t10.setText(last_darb[0+(10*(sarasoPuslapis-1))].data);

    t1.setText(last_darb[1+(10*(sarasoPuslapis-1))].vardPav);
    t11.setText(last_darb[1+(10*(sarasoPuslapis-1))].data);

    t2.setText(last_darb[2+(10*(sarasoPuslapis-1))].vardPav);
    t12.setText(last_darb[2+(10*(sarasoPuslapis-1))].data);

    t3.setText(last_darb[3+(10*(sarasoPuslapis-1))].vardPav);
    t13.setText(last_darb[3+(10*(sarasoPuslapis-1))].data);

    t4.setText(last_darb[4+(10*(sarasoPuslapis-1))].vardPav);
    t14.setText(last_darb[4+(10*(sarasoPuslapis-1))].data);

    t5.setText(last_darb[5+(10*(sarasoPuslapis-1))].vardPav);
    t15.setText(last_darb[5+(10*(sarasoPuslapis-1))].data);

    t6.setText(last_darb[6+(10*(sarasoPuslapis-1))].vardPav);
    t16.setText(last_darb[6+(10*(sarasoPuslapis-1))].data);

    t7.setText(last_darb[7+(10*(sarasoPuslapis-1))].vardPav);
    t17.setText(last_darb[7+(10*(sarasoPuslapis-1))].data);

    t8.setText(last_darb[8+(10*(sarasoPuslapis-1))].vardPav);
    t18.setText(last_darb[8+(10*(sarasoPuslapis-1))].data);

    t9.setText(last_darb[9+(10*(sarasoPuslapis-1))].vardPav);
    t19.setText(last_darb[9+(10*(sarasoPuslapis-1))].data);


  }


}

void downButtonPopCallback(void *ptr)
{
  if (sarasoPuslapis != 1)
  {
    sarasoPuslapis--;

    //Atvaizduojami kiti 10 darbuotoju
    t0.setText(last_darb[0+(10*sarasoPuslapis-1)].vardPav);
    t10.setText(last_darb[0+(10*sarasoPuslapis-1)].data);

    t1.setText(last_darb[1+(10*sarasoPuslapis-1)].vardPav);
    t11.setText(last_darb[1+(10*sarasoPuslapis-1)].data);

    t2.setText(last_darb[2+(10*sarasoPuslapis-1)].vardPav);
    t12.setText(last_darb[2+(10*sarasoPuslapis-1)].data);

    t3.setText(last_darb[3+(10*sarasoPuslapis-1)].vardPav);
    t13.setText(last_darb[3+(10*sarasoPuslapis-1)].data);

    t4.setText(last_darb[4+(10*sarasoPuslapis-1)].vardPav);
    t14.setText(last_darb[4+(10*sarasoPuslapis-1)].data);

    t5.setText(last_darb[5+(10*sarasoPuslapis-1)].vardPav);
    t15.setText(last_darb[5+(10*sarasoPuslapis-1)].data);

    t6.setText(last_darb[6+(10*sarasoPuslapis-1)].vardPav);
    t16.setText(last_darb[6+(10*sarasoPuslapis-1)].data);

    t7.setText(last_darb[7+(10*sarasoPuslapis-1)].vardPav);
    t17.setText(last_darb[7+(10*sarasoPuslapis-1)].data);

    t8.setText(last_darb[8+(10*sarasoPuslapis-1)].vardPav);
    t18.setText(last_darb[8+(10*sarasoPuslapis-1)].data);

    t9.setText(last_darb[9+(10*sarasoPuslapis-1)].vardPav);
    t19.setText(last_darb[9+(10*sarasoPuslapis-1)].data);
  }

}
//------------------------------------ mygtuku metodu pabaiga --------------------------


// void setup() vykdomas viena karta paleidus arduino, nu tipo maitinima padavus
// tai čia visokiu vienkartinius nustatymus padarom
// tokius kaip objektų paleidimus (nežinau kaip pasakyt)

void setup() {
  // paleidziamas ekranas
  nexInit();



  // nuskaitomi duomenys is sd korteles
  SD.begin(D8);
  skaitymas("data.txt", darb_count);

  // rasymas i eeprom
  EEPROM.begin(8);
  skaitytiEEPROM();

  // paleidziamas laikrodis
  Wire.begin(0, 2);
  Laikrodis.Begin();

  // patikrinam ar laikrodis nenusinulines
  if (!Laikrodis.IsDateTimeValid())
  {
    RtcDateTime inValid = RtcDateTime(2001, 1, 1, 0, 0, 0);
    Laikrodis.SetDateTime(inValid);
  }

  // paleidziam skaitytuva
  skaitytuvas.begin(5, 4);

  // iskart atnaujinam laika ir data
  atnaujinti_Laika_Iskart();

  //pridedami attachPop arba attachPush
  //funkcijos argumentas kita void funkcija, kurioje vyks veiksmai paspaudus mygtuka.
  //situ pats nelabai suprantu, bet tiesiog šabloniškai kiekvienam mygtukui jie
  //mygtukas gali siųst komandą kai tik paliečiamas arba kai atleidžiamas
  //visur manau reiks naudot kai atleidžiamas (Pop)
  //jei norėtumėm kai paliečiamas tai visur vietoj Pop rašom Push (pvz:. nustatymai.attachPush(nustatymaiPushCallback);)
  //tam reikia aišku nextion edditoryje mygtuko nustatymuose parinkt varnele prie touch release event arba prie touch press event
  //ta varnelė reiškia kad spaudimo arba atleidimo metu išsius komanda per serial

  // 0 puslapio
  nustatymai.attachPop(nustatymaiPopCallback);
  laikas.attachPop(laikasPopCallback);
  data.attachPop(dataPopCallback);
  sarasas.attachPop(sarasasPopCallback);
  // 1 puslapio
  namai1.attachPop(namai1PopCallback);
  b1.attachPop(b1PopCallback);
  b2.attachPop(b2PopCallback);
  b3.attachPop(b3PopCallback);
  b4.attachPop(b4PopCallback);
  b5.attachPop(b5PopCallback);
  b6.attachPop(b6PopCallback);
  b7.attachPop(b7PopCallback);
  b8.attachPop(b8PopCallback);
  b9.attachPop(b9PopCallback);
  b10.attachPop(b10PopCallback);
  trinti.attachPop(trintiPopCallback);
  patvirtinti.attachPop(patvirtintiPopCallback);
  // 2 puslapio
  namai2.attachPop(namai2PopCallback);
  plusMetai.attachPop(plusMetaiPopCallback);
  minusMetai.attachPop(minusMetaiPopCallback);
  plusMenesis.attachPop(plusMenesisPopCallback);
  minusMenesis.attachPop(minusMenesisPopCallback);
  plusDiena.attachPop(plusDienaPopCallback);
  minusDiena.attachPop(minusDienaPopCallback);
  patvirtinti_data.attachPop(patvirtinti_dataPopCallback);
  // 3 puslapio
  namai3.attachPop(namai3PopCallback);
  plusVal.attachPop(plusValPopCallback);
  minusVal.attachPop(minusValPopCallback);
  plusMin.attachPop(plusMinPopCallback);
  minusMin.attachPop(minusMinPopCallback);
  patvirtinti_laika.attachPop(patvirtinti_laikaPopCallBack);
  // 4 puslapio
  namai4.attachPop(namai4PopCallback);
  patvirtinti_nust.attachPop(patvirtinti_nustPopCallback);
  // 5 puslapio
  namai5.attachPop(namai5PopCallback);
  // 6 puslapio
  namai6.attachPop(namai6PopCallback);
  upButton.attachPop(upButtonPopCallback);
  downButton.attachPop(downButtonPopCallback);
}

//--------------------------------------------------------funkcijos rašytos mūsų-------------------------------

//atnaujina laika iskart iskvietus metoda ir esant pradiniame puslapyje
void atnaujinti_Laika_Iskart()
{
  RtcDateTime dabar = Laikrodis.GetDateTime();
  buves_min = dabar.Minute();
  val.setValue(dabar.Hour());
  minut.setValue(dabar.Minute());
  RtcTemperature t = Laikrodis.GetTemperature();
  String temperatura = String( t.AsFloatDegC(), 0) + String("*C");
  char buferis[6];
  temperatura.toCharArray(buferis, 6);
  temp.setText(buferis);
  char datestring[11];
  snprintf_P(datestring,
             countof(datestring),
             PSTR("%04u-%02u-%02u"),
             dabar.Year(),
             dabar.Month(),
             dabar.Day());
  dat.setText(datestring);
}

//atnaujina laika kas minute
void atnaujinti_Laika()
{
  if (esamas_puslapis == 0) {
    if (esamas_puslapis != buves_puslapis)
    {
      atnaujinti_Laika_Iskart();
      buves_puslapis = esamas_puslapis;
      return;
    }
    unsigned long db = millis();
    if (db - buves_laikas >= interval)
    {
      buves_laikas = db;
      RtcDateTime dabar = Laikrodis.GetDateTime();
      if (buves_min != dabar.Minute())
      {
        buves_min = dabar.Minute();
        val.setValue(dabar.Hour());
        minut.setValue(dabar.Minute());
        RtcTemperature t = Laikrodis.GetTemperature();

        String temperatura = String(t.AsFloatDegC(), 0) + String("*C");
        char buferis[6];
        temperatura.toCharArray(buferis, 6);
        temp.setText(buferis);
        char datestring[11];
        snprintf_P(datestring,
                   countof(datestring),
                   PSTR("%04u-%02u-%02u"),
                   dabar.Year(),
                   dabar.Month(),
                   dabar.Day());
        dat.setText(datestring);
      }
    }
  }
}

//nuskaitom nustaymus
void skaitytiEEPROM()
{
  log_size = EEPROM.read(0);
  power_save = EEPROM.read(1);
}

//surasti darbuotoja sarase
int surastiDarbuotoja(unsigned long id)
{
  for (int i = 0; i < darb_count; i++)
    if (id == A[i].id)
      return i;
  return -1;
}

//tikrinama ar nuskaityta kortele bei atliekami veiksmai jeit taip
void arNuskaityta()
{
  if (skaitytuvas.available())
  {

    int indeksas = surastiDarbuotoja(skaitytuvas.getCode());
    if (indeksas != -1)
    {
      if (esamas_puslapis != 5)
      {
        buves_puslapis = esamas_puslapis;
        esamas_puslapis = 5;
        page5.show();
        memset(pin_vedamas, 0, sizeof(pin_vedamas));
        indx = 0;
      }
      pranesimas.setText(A[indeksas].vardPav);
      buves_laikas1 = millis();
      // irasom i sd kortele laika ir duomenis
      //irasom i masyva atvaizdavimo

      /*darb_vaizdavimas tarp;                                         //tarpinis kint
        memcpy(tarp.vardPav, A[indeksas].vardPav, 30 * sizeof(char));  //reikia tvarkyt
        //patikrint ar sitas veikia
        Darb_iterpimas(tarp);*/
    }
  }
}

void grazintiAtgal()
{
  if (esamas_puslapis == 5)
  {
    unsigned long db = millis();
    if (db - buves_laikas1 >= intervalas1)
    {
      int tarpinis = buves_puslapis;
      buves_puslapis = esamas_puslapis;
      esamas_puslapis = tarpinis;
      switch (esamas_puslapis)
      {
        case 0 :
          page0.show();
          break;
        case 1 :
          page1.show();
          break;
        case 2 :
          page2.show();
          break;
        case 3 :
          page3.show();
          break;
        case 4 :
          page4.show();
          break;
        case 6 :
          page6.show();
          break;
      }
    }
  }
}


// Skaitymas pradiniu duomenu is sd korteles
// Duomenys rasomi i darbuotoju strukturos masyva
// funkcija naudojama tik  viena karta paleidus irengini
// duomenu failo formatas:
/*
   vardas pavarde;00000000
*/
// jokiu tarpo simboliu bei pacioje pabaigoje jokio \n simbolio
bool skaitymas(char duom_failas[], int &n)
{
  failas = SD.open(duom_failas);
  if (failas) {
    bool laukas = 0;
    int i = 0;
    n = 0;
    char id[8];

    while (failas.available())
    {
      char tarp = failas.read();
      if (tarp != '\n')
      {
        if (tarp == ';')
        {
          laukas = 1;
          i = 0;
        }
        else if (laukas == 0)
          A[n].vardPav[i++] = tarp;
        else
          id[i++] = tarp;
      }
      else
      {
        A[n].id = strtoul(id, NULL, 0);
        laukas = 0;
        i = 0;
        n++;
      }
      if (!failas.available())
        A[n++].id = strtoul(id, NULL, 0);
    }
    failas.close();
    return true;
  } else {
    return false;
  }
}

// Darbuotoju iterpimas ir istrynimas is masyvo
// Naudojama atvaizdavimui ekraniuke
// Paskutiniu 30 prisiregistravimu
int Darb_iterpimas(darb_vaizdavimas naujas)
{
  if (counteris == 30)
  {
    for (int i = counteris - 1; i > 0; i--)
    {
      last_darb[i] = last_darb[i - 1];
    }
    last_darb[0] = naujas;
  }
  else
  {
    for (int i = counteris; i > 0; i--)
    {
      last_darb[i] = last_darb[i - 1];
    }
    last_darb[0] = naujas;
    counteris++;
  }
  return counteris;
}
//-------------------------------------------------------------------------------------------------------------

// void loop vykdomas be sustojimo po void setup()
// vykdoma greit
// svarbu nenaudot delay() funkcijų, nes jos sustabdo darbą ir kiti procesai sustoja tam laikui
// jei ką norit atidėt naudokit millis(). Info google, tkr labai paprasta suprast
// arba galit naudot realaus laikrodzio modulio sekundes jei uztenka atidėt kažką iki sekundės
void loop() {
  nexLoop(nex_listen_list);  // ciklas kuris žiūri ar kažkas paspausta
  atnaujinti_Laika();
  arNuskaityta();
  grazintiAtgal();
}

