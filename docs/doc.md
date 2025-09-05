# Inhaltsverzeichnis

- [Warum gibt es Jiffy?](#warum-gibt-es-jiffy)
- [Anforderungen](#was-soll-jiffy-können)
  - [Zeiterfassung](#zeiterfassung)
  - [Auswertung](#auswertung)
  - [Klassen](#klassen)
  - [Aktivitäten](#aktivitäten)
- [Use Cases](#use-cases)
  - [Zeiterfassung](#zeiterfassung-1)
  - [Aktivitätenverwaltung](#aktivitätenverwaltung)
  - [Klassenverwaltung](#klassenverwaltung)
  - [Aktivitäten](#aktivitäten)

## Warum gibt es Jiffy?
Der Zweck von Jiffy ist es, den Zeitaufschrieb an der IMS-T so weit wie möglich zu vereinfachen und die unnötige Komplexität zu eliminieren, die durch die vielen individuell von den Schülern erstellten Excel-Sheets zur Zeiterfassung entstanden ist. Jeden Monat müssen die Schüler ihre Stunden abgeben, was häufig zu Problemen führt: Für die Schüler ist es mühsam, die Stunden immer wieder einzureichen, und für die Lehrpersonen ist es ebenso anstrengend, da sie viele unterschiedliche Designs der Zeitaufschriebe verstehen und jede einzelne Datei separat öffnen müssen.

Mit Jiffy soll dieser Prozess deutlich einfacher und einheitlicher gestaltet werden. Alle Zeitaufschriebe können zentral erfasst und ausgewertet werden, ohne dass für jeden Schüler eine neue Excel-Tabelle notwendig ist. Stattdessen kann direkt nach dem jeweiligen Schüler gesucht werden, wodurch der gesamte Ablauf effizienter und übersichtlicher wird. Zudem bietet Jiffy die Möglichkeit, anstatt mühsam Start- und Endzeiten manuell einzutragen, einfach zu beschreiben, was man gerade macht, auf Start zu drücken und am Schluss einfach auf Stop zu klicken.

## Was soll Jiffy können?
### **Zeiterfassung**
- **Stoppuhr-prinzip**: Die Zeit sollte so einfach wie möglich erfasst werden, idealerweise nach dem Stoppuhr-Prinzip. Man drückt einfach auf Start und auf Stop, sobald man fertig ist.

- **Löschen von Einträgen**: Versehentlich eingetragene Einträge sollten wieder gelöscht werden können.

- **Bearbeiten**: Einträge, die nicht der Realität entsprechen, sei dies durch versehentliches stoppen, falsche Aktivitäten oder weil vergessen wurde, den Eintrag zu stoppen, sollten alle Eigenschaften bearbeitet werden können.

### **Auswertung**
- **Überzeiten**: Die Über- oder Unterstunden sollen automatisch aus den erfassten Zeiten und der Tagessollzeit berechnet werden, die im jeweiligen Eintrag hinterlegt ist. Das Ergebnis sollte einfach und übersichtlich dargestellt werden.

- **Krankheitstage**: Krankheitstage sollen gezählt, dargestellt und bei der Berechnung der Über- oder Unterstunden nicht berücksichtigt werden.

- **Vergangene Einträge**: Alle bisherigen Einträge sollen übersichtlich und in chronologischer Reihenfolge einsehbar sein, sprich alle wichtigen Details sollten auf einen Blick ersichtlich sein.

- **Filterfunktion**:  Die Anzeige der vergangenen Einträge sollte gefiltert werden können, um nur Einträge anzuzeigen, die bestimmte Kriterien erfüllen, damit man nach z. B. bestimmten Aktivitäten suchen kann.

- **Administrator-Ansicht**: Administratoren sollen die Zeiterfassung aller Nutzer einsehen können und dabei auch Zugriff auf alle weiteren Funktionen der Auswertungsseite haben.

### **Klassen**
- **Klasseauswahl**: Die Nutzer sollten sich ihre eigene Klasse aussuchen können.

- **Tagessollzeiten**: Die Klassen sollen eine Tagessollzeit hinterlegen können. Bei der Erstellung eines neuen Eintrags wird die Sollzeit, die zur Klasse des Nutzers sowie zum aktuellen Datum bzw. Wochentag gehört, im Eintrag gespeichert. So wird sichergestellt, dass die Berechnung der Über- oder Unterstunden auch dann korrekt bleibt, wenn sich die Sollzeiten (z. B. nach den Sommerferien durch bearbeitung der Klasse) ändern.

- **Klassenverwaltung**: Der Administrator soll Klassen erstellen, bearbeiten und löschen können. Dabei soll es möglich sein, diesen Klassen entsprechende Tagessollzeiten zuzuweisen.

## **Aktivitäten**
- **Verlauf**: Beim Erstellen eines neuen Eintrags soll der Nutzer die Möglichkeit haben, auszuwählen, ob er einen neuen Titel/eine neue Beschreibung für seine Aktivitäten erstellen oder eine bereits verwendete auswählen möchte. Beim Erstellen des Eintrags werden automatisch die neue Titel/Beschreibungen hinzugefügt, falls diese noch nicht vorhanden sind.

- **Favorisierung**: Es soll möglich sein, bestimmte Aktivitäten als Favoriten zu markieren. Diese sollten dann im Verlauf ganz oben angezeigt werden.

- **Verwalten**: Die Aktivitäten sollten verwaltet werden können, das heisst, sie sollten favorisiert, gelöscht und bearbeitet werden können.


## Use Cases

![Use Cases](../assets/UseCases.svg)
