# Test Technique - Henri Trip (HWS Agency)

Ce projet contient la partie **Backend** du test technique pour le rôle de développeur Fullstack chez Henri Trip. L'objectif est de fournir une API robuste pour la gestion de guides de voyage et d'activités.

---

## 🏗 Choix Techniques & Architecture

J'ai choisi d'implémenter ce projet en **ASP.NET Core** en suivant strictement les principes de la **Clean Architecture**.

### Pourquoi la Clean Architecture ?
- **Séparation des responsabilités** : Le domaine (logique métier) est totalement indépendant des frameworks externes.
- **Maintenabilité** : Facilite l'évolution du code et l'ajout de nouvelles fonctionnalités.
- **Testabilité** : Permet de tester la logique métier sans dépendre de la base de données.

### Stack Utilisée
- **Framework** : .NET 8
- **Base de données** : MySql / Entity Framework Core
- **Sécurité** : ASP.NET Identity (JWT) pour la gestion des rôles Admin et User.

---

## 🛠 Structure du Projet

Le projet est divisé en 4 couches physiques :
1. **Domain** : Entités `Guide`, `Activity`, `User` et Enums métier.
2. **Application** : Logique de filtrage (permissions) et DTOs.
3. **Infrastructure** : Persistance (EF Core) et Identity.
4. **API** : Points d'entrée (Controllers) et documentation Swagger.

---

## 🚀 Installation et Lancement

### Prérequis
- .NET SDK (version 8.0+)
- Une instance SQL Server (ou locale)

### Étapes
1. Cloner le repository :
   ```bash
   git clone [Lien de ton repo]
    cd HwsBackend
    ```

2. Configurer la chaîne de connexion dans appsettings.json.

3. Appliquer les migrations :
   ```bash
   dotnet ef database update --project HwsBackend.Infrastructure --startup-project HwsBackend.Api
   ```
4. Lancer l'API :
   ```bash
   dotnet run --project HwsBackend.Api
   ```
5. Accéder à Swagger pour tester les endpoints :
   ``` 
   http://localhost:5000/swagger
    ```

## 🔐 Gestion des Permissions (Règles Métier)
- **Admin** : Accès complet à tous les guides et activités.
- **User** : Accès uniquement aux guides auxquels ils sont invités (via `InvitedUserIds`).
- **Activités** : Chaque activité est liée à un jour spécifique et possède un ordre de visite précis.

## 📝 Fonctionnalités implémentées
- CRUD complet des Guides et Activités (Admin).

- Gestion des utilisateurs et rôles.

- Filtrage sécurisé des guides par invitations (User).

- Documentation interactive via Swagger.
