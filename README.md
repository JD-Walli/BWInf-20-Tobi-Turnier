# BwInf-39.1.3-Tobi-Turnier

Bearbeitung der Aufgabe "Tobis Turnier" aus dem 39ten Bundeswettbewerb Informatik.

## Aufgabe

Tobi veranstaltet für seine Freunde ein Turnier für
das Zweipersonenspiel RNG. Bei einer Partie RNG
gewinnt nicht immer der bessere Spieler.
Deshalb fragt sich Tobi, wie er das Turnier durchführen
sollte. Er könnte beispielsweise eine Liga
durchführen oder im K.o.-System spielen lassen. Nun
möchte er herausfinden, welche Turniervariante
sich am besten eignet, um den insgesamt besten
Spieler herauszufinden.
Tobi überlegt sich folgendes Zufallsexperiment, um
ein Spiel bezüglich seines Ausgangs zu simulieren:
Jeder Spieler hat eine Spielstärke, die durch eine
Zahl zwischen 0 und 100 ausgedrückt wird. Beide
Spieler legen Kugeln in eine Urne, und zwar so
viele, wie ihre Spielstärke hoch ist. Dann wird eine
Kugel zufällig aus der Urne gezogen, wobei jede
Kugel die gleiche Chance hat. Der Besitzer der gezogenen
Kugel gewinnt das Spiel.

Auf den BWINF-Webseiten findest du Beschreibungen
verschiedener Turniervarianten. Schreibe
ein Programm, das die Spielstärken der Spieler einliest
und für jede dieser Turniervarianten ermittelt,
wie oft der spielstärkste Spieler im Durchschnitt
über viele Wiederholungen des Turniers gewinnt.
Empfiehl Tobi eine Turniervariante auf Grundlage
deiner Ergebnisse.

## Lösungsansatz

Die Idee war es, für jede Turnierart eine Funktion zu schreiben, die das Turnier einmal durchspielt und dann den Gewinner zurückgibt. Diese Funktionen können dann jeweils mehrmals aufgerufen werden und die Gewinnwahrscheinlichkeit für den besten Spieler kann berechnet werden. Anhand dieser Wahrscheinlichkeit kann herausgefunden werden, welche Methode sich am Besten eignet.

Verwendete Turnierarten:

Liga: Beim Ligaspiel spielt jeder Spieler mit jedem anderen Spieler. Der Spieler mit den meisten Gewinnen ist der Sieger.

kO Spiel: Pärchenweise werden Ausscheidungsspiele gespielt (in einer Variante fünf mal nacheinander). Der Gewinner rückt vor in die nächste Runde. 
