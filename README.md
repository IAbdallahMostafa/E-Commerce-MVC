# 🛒 E-Commerce MVC

A modern and scalable E-Commerce web application built with **ASP.NET Core MVC**, implementing layered architecture, repository pattern, and payment integration using **Stripe**. The application covers essential e-commerce functionalities such as product management, cart, user authentication, and order checkout.

---

## 📁 Project Structure

```
E-Commerce/
│
├── E-Commerce.Web           # ASP.NET Core MVC Web Application (Presentation Layer)
├── E-Commerce.DataAccess    # Data access layer (EF Core, Repositories, Migrations)
├── E-Commerce.Entities      # Entities and Interfaces (Domain Layer)
├── Utilities                # Common helpers and utilities
├── E-Commerce.sln           # Solution File
```

---

## 🚀 Features

- ✅ User Registration and Login (with ASP.NET Identity)
- ✅ Product Listing, Details, and Filtering
- ✅ Add to Cart & Manage Cart
- ✅ Order Summary & Checkout
- ✅ Stripe Payment Integration 💳
- ✅ Admin Panel for Product Management
- ✅ Database Initialization & Seeding


---

## 🔌 Technologies Used

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- SQL Server
- Stripe API
- SweetAlert (for UI alerts)
- Bootstrap 5
- AutoMapper
- Repository & Unit of Work pattern

---

## 💳 Stripe Integration

Stripe has been integrated for secure and real-time payment processing during checkout.  
To use your own Stripe keys, update the keys in your `appsettings.json` or `secrets.json`:

```json
"Stripe": {
  "SecretKey": "your_stripe_secret_key",
  "PublishableKey": "your_stripe_publishable_key"
}
```

---

## ⚙️ Setup Instructions

1. **Clone the Repository**

```bash
git clone https://github.com/IAbdallahMostafa/E-Commerce-MVC.git
cd E-Commerce
```

2. **Update Database**

```bash
# From the E-Commerce.Web project directory
dotnet ef database update
```

3. **Run the Application**

```bash
dotnet run --project E-Commerce.Web
```

4. **Browse the App**

Visit `https://localhost:xxxx` in your browser.

---

## 👨‍💻 Admin Access

To access admin features (like managing products), seed a user with the Admin role in the database initializer or register a user and assign the Admin role manually.

---

## 📌 Todo

- [ ] Add email confirmation
- [ ] Add customer order history
- [ ] Add unit/integration tests

---

## 🤝 Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

---

## 📄 License

This project is open-source and available under the [MIT License](LICENSE).

---

## ✨ Author

**Abdallah Mostafa**  
[GitHub](https://github.com/IAbdallahMostafa) | [LinkedIn](https://www.linkedin.com/in/Iabdallahmostafa/)
