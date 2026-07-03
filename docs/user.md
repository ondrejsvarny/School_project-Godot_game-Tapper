# Uživatelská dokumentace

## Úvod
Tapper je 2D hra, kde hráte za barmana, ktorý čapuje pivo a posiela ho po pultoch zákazníkom. Je to pôvodne arkádová hra z 80. rokov, ktorej rôzne verzie neskôr vyšli aj na počítače ZX Spectrum a iné platformy. Toto je remake pre moderné platformy s The Simpsons tematikou. Je to "endless" hra, v ktorej zákazníci pribúdajú postupne čoraz rýchlejšie a vašim cieľom je získať čo najvyššie skóre.

## Spustenie hry
Hru na zariadení s operačným systémom Windows spustíte otvorením aplikácie `tapper.exe`. Aplikáciu si môžete stiahnuť zo zložky `export` z tohto repozitára. Hra sa spustí v režime Fullscreen. 

## Ovládanie 
Hra začne v hlavnom menu, kde jednodnoducho po kliknutí `Play` začnete hrať. `Exit` ukončí celú aplikáciu. 

V hre hráte za barmana, ovládať ho viete nasledujúcimi klávesami:
* `W` alebo `↑` - posun na vyšší bar
* `S` alebo `↓` - posun na nižší bar
* `A` alebo `←` - posun pozdĺž baru doľava
* `D` alebo `→` - posun pozdĺž baru doprava
* `E` alebo `Medzerník` - čapovanie a hodenie piva (funkčné iba pri výčape)

Po skončení hry skončíte v koncovom menu, kde pomocou `Try again` viete spustiť hru znovu alebo sa viete pomocou `Exit to menu` vrátiť do hlavného menu. V tomto menu tiež vidíte svoje skóre z poslednej hry a najlepšie skóre z predošlých hier. Najlepšie skóre sa vám ukladá aj po vypnutí aplikácie. Pre jeho prípadné zmazanie môžete zmazať súbor `savegame.save` bežne uložený v lokácii: `C:\Users\[Vaše meno]\AppData\Roaming\Godot\app_userdata\Tapper\`.

## Priebeh hry
### Hádzanie pív
Počas hry budú zľava výchádzať zákazníci a vašou úlohou je ich obsluhovať. To robíte tak, že pri danom pulte prídete k výčapu, načapujete pivo a hodíte ho zákazníkovi. Počas čapovanie a hádzania na krátky moment nemôžete robiť nič iné. Chytenie piva zákazníka posunie späť. Cieľom je každého zákazníka "vytlačiť von", z kade prišiel, tým že mu hodíte pivo keď je blízko východu. 

### Chytanie pohárov
Keď zákazík zákazník vypije pivo, vzápätí vám hodí spáť prázdny pohár. Vašou ďalšou úlohou je všetky prázdne poháre chytať, skôr než dôjdu na koniec pultu a rozbijú sa. Po východe zákazník už prázdny pohár nehodí. Prázdne poháre chytíte buď tak, že stojíte pri výčape daného pultu, alebo k poháru pribehnete pozdĺž pultu. Prejsť na vyšší/nižší bar môžete odkiaľkoľvek. 

### Sprepitné 
Pokiaľ zákazíka obslúžite dostatočne rýchlo, odkedy prišiel a zároveň ho nevytlačíte von, je šanca, že vám nechá na pulte sprepitné. Poň viete prísť pozdĺž pultu a zobrať ho.

### Zbieranie bodov
Hlavným cieľom hry je nazbierať čo najviac bodov. To sa dá dvomi spôsobmi:
* Zákazník chytí pivo - 1 bod
* Zoberiete sprepitné - 3 body

Body sa zobrazujú vpravo hore počas hry a na koncovej obrazovke. 

### Životy a koniec hry
Zákazníci v priebehu hry budú chodiť čoraz rýchlejšie. Máte k dispozícii 3 životy - pivá vľavo hore. Po ich stratení je hra ukončené. Život sa dá stratiť viacerými spôsobmi:
* Hodíte pivo na pult, kde nikto nie je
* Rozbije sa prázdny pohár
* Zákazník dôjde na koniec pultu, vtedy začne padať pod pult a máte krátky moment mu hodiť pivo a ho ešte zachániť, inak stratíte život

