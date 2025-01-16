
<p align="center">
  <img src="https://github.com/user-attachments/assets/303cf11c-40ba-49fb-b8f2-5be075ec4e3a" alt="image" width="50%">
</p>


# Stas.Monitor 

>Projet B2-UE Programmation avancée a l'H.E.L.Mosane en lien avec le repository `Stas.Thermometer`
>
>
Stas.Monitor est une application .NET 6 utilisant Avalonia 11+ qui récupère les données de mesures de température et d'humidité depuis une base de données relationnelle et les affiche dans une interface graphique. Ce projet permet de visualiser en temps réel les mesures provenant des différents thermomètres configurés dans `Stas.Thermometer` et propose des options de filtrage pour l'affichage des résultats.

![image](https://github.com/user-attachments/assets/b6bdbe81-181c-48ec-bf0e-99486fc4c13e)


## Fonctionnalités principales

![image](https://github.com/user-attachments/assets/36807ac9-7fb5-4ceb-afe5-48fc30c476db)


- **Affichage des mesures** : Le moniteur affiche les dernières mesures de température et d’humidité provenant de la base de données. Il est capable de lire et de mettre à jour l’affichage en temps réel.
- **Gestion des alertes** :  Les alertes définies dans `Stas.Thermometer` sont également affichées et permettent à l'utilisateur de visualiser les seuils dépassés pour chaque thermomètre.
- **Filtrage des données** : Des options sont disponibles pour filtrer l'affichage des données en fonction des thermomètres, des types de mesures et du temps (température, humidité, etc.).


 ## Particularité Technique du projet
 - **Plateforme** : C# cible .NET 6 et Avalonia 11+ pour l’interface graphique multiplateforme.
 - **Exploitation de spécificités C#** : Utilisation de `async/await`, des expressions lambda, des delegates, de Linq et des événements pour la gestion des données.
 - **Tests unitaires** : couverture a 100%Domains et 87%Presenter, utilisation de Mocks
 - **DP utilisés** : DP "Adapter" 
![image](https://github.com/user-attachments/assets/ddc94303-bb58-48aa-8336-bed9225bd8eb)



## Structure des Données

Les données affichées dans Stas.Monitor proviennent directement des tables `Mesures` et `Alerts` dans la base de données définie par `Stas.Thermometer`.

### Table `Mesures`

```sql
CREATE TABLE Mesures (
    id INT AUTO_INCREMENT PRIMARY KEY,
    thermometerName VARCHAR(255) NOT NULL,
    datetime DATETIME NOT NULL,
    type VARCHAR(50) NOT NULL,
    format VARCHAR(50) NOT NULL,
    value DOUBLE NOT NULL
);
```

### Table `Alerts`
```sql
CREATE TABLE Alerts (
    id INT AUTO_INCREMENT PRIMARY KEY,
    expectedValue DOUBLE NOT NULL,
    idMesure INT NOT NULL,
    FOREIGN KEY (idMesure) REFERENCES Mesures(id)
);
```

## Instructions d'installation et d'exécution

1. Clonez le dépôt :
   ```bash
   git clone <url-du-dépôt>
   ```
2. Configurez le fichier `config.ini` pour spécifier les paramètres de connexion à la base de données et les thermomètres à afficher :
   ```ini
   [general]
   thermometer1 = Living Room 1
   thermometer2 = salon
   thermometer3 = chambre
   [BD]
   IpServer= your.ipaddress.com
   PortServer= 8081
   User= USERNAME
   Pws= PASSWORD
   ```
   **Note** : Assurez-vous que les thermomètres dans la section [general] correspondent à ceux définis dans votre base de données.
   
3. Exécutez l'application avec la commande suivante :
   ```bash
   dotnet run -- --config-file config.ini
   ```
