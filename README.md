# Zápočtový program: *Tapper*

## Specifikace

Môj projekt je hra Tapper. Je to pôvodne arkádová hra z 80. rokov, ktorej rôzne verzie neskôr vyšli aj na počítače ZX Spectrum a iné platformy. Ide o hru, kde hráte za barmana, ktorý čapuje pivo a posiela ho po pultoch zákazníkom, popri tom musíte chytať prázdne poháre a často je doplnená ďalšími mechanikami a bonusmi. 

V mojej verzii som v skratke implementoval: 4 barové pulty a pohyb hráča (barmana) hore/dole medzi barmi a pozdĺž barov, spawnovanie a pohyb zákazníkov pozdĺž pultov, čapovanie a hádzanie pív zákazníkom, chytanie prázdnych pohárov, systém 3 životov a situácie ktoré život uberú, spawnovanie sprepitného atď. 

## Instalace a spuštění

Hra je vytvorená v game engine Godot. Pre jednoduché spustenie vyexportovanej hry vo Windowse stačí stiahnuť a rozbaliť `tapper_export.rar` zo zložky `export` a spustiť  `Tapper.exe`. Pre otvorenie projektu v Godote, treba mať stiahnutý `Godot Engine v4.6.1 mono` a importnúť v ňom zložku `TAPPER`. 

## Dokumentace

* [Uživatelská dokumentace](docs/user.md)
* [Programátorská dokumentace](docs/programmer.md)
