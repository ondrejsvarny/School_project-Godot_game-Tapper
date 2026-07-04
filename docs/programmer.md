# Programátorská dokumentace

## Úvod 
Hra je vytvorená v game engine Godot v4.6.1 mono (.NET). Hra je písana v jazyku C#. Všetky súbory hry v uložené v zložke `TAPPER`. 

## Súbory hry 

Súbory hry sú organizované do priečinkov. V nich nájdete:
* Scény s príponou `.tscn`, čo sú vlastne stromovo usporiadané zoznamy objektov - Nodes. Node je v Godote základný objekt od ktorého sa dedia všetky ostatné. 
* Kódy `.cs`. V každom z nich spravidla nájdete jednu triedu. Skoro všetky triedy sú `partial class` nejakého objektu z nejakej scény (spravidla toho koreňového), ktorý dedí od nejakého Godot šablónového objektu. Druhú časť tejto triedy automaticky generuje Godot a sú to potrebné prepojenia s enginom etc.
* Súbory s príponou `.uid` sú identifikátory, ktoré Godot automaticky vytvorí každému scriptu. Podľa nich Godot rozpoznáva súbory aj po premenovaní alebo premiestnení. 
* Assety - obrázky `.png`, font `.tff`, ikona `.ico` etc.

Ostané veci ako súbory `.import` alebo skrytú zložku `.godot` si Godot automaticky vytvorí pri štarte a na Gite sú ignorované. 

## Objekty (scény) a príslušné triedy
Tu podrobnejšie popíšem čo je v každej zložke a na čo to slúži.

### Zložka Levels
V nej je iba jedna scéna `level.tscn` - je to hlavná hra. Keby bolo levelov viac, môžu sa uložiť tu. Je to hlavná scéna, v jej strome sa nachádzajú (alebo sa počas hry vytvoria) všetky hlavné herné objekty. Koreňom je základný *Node2D*. Level je poskladaný modulárne, napr. stoly sú poskladané z častí, všetky vždy uložené pod jedným `Node2D`, vždy obsahujú `TableEnd` a `TableStart`. Každý objekt má nejaký Z Index, ktorý hovorí ako "vysoko" sa vykreslí, niektoré ho majú nastavený v Inspectore, niektoré nastavujem v stripte pri vytváraní. Script `Level.cs` je len resetovanie životov a skóre na začiatku hry. 

### Zložka Player
V nej je takisto iba jedna scéna, a to hráč (barman) - `player.tscn`. Koreňový Node je *CharacterBody2D*, ktorý má fyziku a vhodné vlastnosti na to byť objekt ovládaný hráčom. V strome má objekty *CollisionShape2D* aby sme zaznamenali kolízie s okrajmi, pohárom etc. , *Sprite2D* je textúra ktorá má v sebe celý spritesheet a *AnimationPlayer* tvorí animácie tým že prepína RegionRect v *Sprite2D* na vhodných miestach. *Marker2D* je vlastne lokácia, kde sa budú spawnovať pivá. 

Script `Player.cs` rieši polohu a pohyb hráča. Do [Export] premenných netreba v Godot editore zabudnúť dať odkaz na BeerScene a pole *Marker2D* BarPositions ktoré v leveli určujú miesta kde sa počas hry premiestni hráč hore/dole pri zmene baru. 

Funkcie `HandleHorizontalMovement()` a `HandleVerticalMovement()` riešia pohyb hráča a sú opakovane volané z `_PhysicsProcess()`, funkcie, ktorej engine zabezpečuje volanie presný počet krát za sekundu - v tomto projekte je to nastavené na 60. Horizontal movement je riešený pomerne klasicky kde pri stlačení klávesu získame `direction` (1,0,-1) a tú hneď aplikujeme `velocity.X = direction * Speed `.

Vertical movement riešime pomocou pola `BarPositions`, kde po stlačení klávesu hráča premiestnime vyššie/nižšie. Pritýchto pohyboch sa spúšťajú príslušné animácie cez AnimationPlayer. Pri tomto pohybe je na daný tick vypnutá interpolácia pomocou `ResetPhysicsInterpolation()`. Tá je inak zapnutá a zapezpečuje plynulé pohyby aj keď prekvencia fyzického ticku enginu nesúladí s vykresľovacou frekvenciou monitoru. Tiež tu voláme funkciu `LeaveMoveAnim()` ktorá skopíruje Sprite2D a AnimationPlayer hráča na mieste kde predtým bol, prehrá animáciu "vzduchu" a potom sa zničí. Takto je to vyriešené preto aby hráč necítil žiadny input lag, kým sa prehráva animácia, ale zároveň aby sa tam nejaká animácia prehrala. 

Tiež sú tam funkcie `StartPouring()` a `ThrowBeer()`, ktoré zabezpečujú prehratie potrebných animácií čapovania a hádzania, vytvorenie novej inštancie scény piva a premiestnenie jej na pozíciu Marker2D. 

### Zložka Objects

Toto je zložka s najviac scénami a príslušnými scriptami. Sú to:

* **Beer** - jednoduchý objekt piva zdedený z `Area2D` s `CollisionShape2D`  kvôli detekovaniu kolízie s zákazníkmi a playerom. Obsahuje tiež potrebný `Sprite2D`. Jeho script obsahuje funkciu `GetCaught()` ktorá zistí či už pivo nebolo chytené niekým iným, oznámi to zákazníkovi a prípadne sa odstráni. Tiež sa odstráni pomocou `End()`, keď dôjde na koniec baru.  Pohyb je riešený klasicky cez `_PhysicsProcess()`, podobne ako pri horizontálnom pohybe playera. 

* **Empty glass** - podobne ako Beer, toto je prázdny pohár ktorí hodí zákazík pohybujúci sa konštantnou rýchlosťou. Na rozdiel od Beer obsahuje AnimationPlayer, ktorý prehrá animáciu rozbitia. Má funkciu `Break()` ktorá sa zavolá po kolízii s koncom stola, prehrá sa animácia, uberie sa život a pohár sa zničí. 

*  **TableEnd** a **TableStart** - 2 jednoduché scény. TableEnd ohraničuje ľavý koniec stola pri dverách, obsahuje `StaticBody2D`, ktoré zabraňuje hráčovi prejsť pomocou fyziky a `Area2D` ktorá detekuje Beer ktoré ujde. TableStart je pravý koniec stola, ktorý obsahuje 2 `Area2D`, jedna na detekciu padnutia prázdneho pohára a jedna na detekciu dôjdenia zákazníka na koniec. 

* **Tap** - objekt výčapu, po získaní signálu o vstupe hráča do jeho `Area2D` zmení hráčovi aktuálny výčap, po odídení mu ho vymaže, player podľa toho vie či môže čapovať. Tiež obsahuje `StaticBody2D`, ktoré zabraňuje hráčovi prejsť a výjsť z mapy. 

* **Tips** - jednoduchý objekt, ktorý sa sám zničí po určitom čase, alebo po zaregistrovaní hráča sa tiež zničí a pripočíta skóre. 

* **Customer** - Obsiahla scéna zákazníka. Pomocou state machine prepína medzi rôznymi stavami zákazníka. Pri vytváraní zabezpečí náhodný vzhľad zákazníka. Reaguje na pivo, ak ho chytí do určitého času od vytvorenia je 50% šanca že nechá Tip (sprepitné). Po dôjdení až na začiatok stola, začne klesať. Pri všetkom sa prehrávajú potrebné animáciu pomocou AnimationPlayer. Po dosiahnutí konca stola z kade prišiel za zničí (premenná `_canGoOut` zabezpečuje aby sa sám nezničil hneď pri prichádzaní ale najskôr keď k nemu príde prvé pivo). 

### Zložka Mechanics
Obsahuje jediný script `CustomerSpawner.cs` ktorý rozširuje `Node` ale nemá žiadnu konkrétnu scénu, v levely ho stačí attachnúť na `Node`, v Inspectore mu dať odkaz na Customer Scene a `Marker2D` Spawn pointy v levely. Mechanika spawnovania zákazníkov podľa niekoľkých premenných randomizovane vždy nastaví `Timer` node ktorý po vypršaní dá signál na spawnutie ďalšieho zákazníka. Vytvorí sa nový objekt zákazníka a nastaví sa mu pozícia náhodne jedného spawn pointu. Hra je "endless", takže medzery medzi spawnovaniami sa zmenšujú o danú hodnotu pri každom naplánovaní. 

### Zložka GlobalScripts
Obsahuje 2 "globálne" `public static` triedy, ktoré nededia z ničoho. Trieda `Global` udržuje len 3 premenné. `Score` a `BestScore` kvôly tomu že ich treba prenášať medzi levelom a koncovým menu. A `NumOfLives` jednoducho preto, lebo sa so mi so životmi najlepšie a najprehľadnejšie pracuje takto. `BestScore` sa vždy po zapnutí hry načíta z savu. 

Trieda `SaveSystem` je trieda ktorá ma funkcie `SaveInt` a `LoadInt` na uloženie a načítanie integeru. Vyriešil som to takto jednoducho, neriešil som žiadne zložitejšie štruktúry, lebo potrebujem ukladať len jediný int - `BestScore`. Využíva sa tu Godot trieda `FileAccess`, ktorá sa stará o ukladanie a šifrovanie dát na hráčove zariadenie. Dáta sa vo Windowse bežne ukladajú do lokácie `C:\Users\[Vaše meno]\AppData\Roaming\Godot\app_userdata\Tapper\` do nami  určeného súboru `savegame.save`. 

### Zložka UI
Táto zložka obsahuje 3 UI scény:

* **Menu** - jednoduché menu s príslušným scriptom, využívajúce Godot Control Nodes s 2 tlačidlami, jedno zmení scénu na Level a druhé ukončuje program. Tlačidlá využívajú `button_menu.tres` - tému ktorá dáva všetkým rovnaký vzhľad.

* **Ui** - GUI, ktoré je v strome Level scény. Zobrazuje aktuálne skóre pomocou `Label`, ktoré obnovuje v `_Process` z `Global.cs`. Tiež zobrazuje životy pomocou troch `TextureRect` ktorým funkcia `UpdateLives()` nastaví parameter `Visible` podľa argumentu ktorý dostane. Zmena životov sa kontroluje v `_Process`, aby sme stále nemuseli volať `UpdateLives()`.

* **EndScreen** - Scéna, ktorá zobrazí `Score` a `BestScore` z Global, pri novom `BestScore` zobrazí `Label` `NewRecord` a uloží ho pomocou `SaveSystem`. Tiež má buttony ako v Menu, ktoré zmenia scénu. 

