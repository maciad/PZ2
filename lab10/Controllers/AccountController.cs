using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace lab10.Controllers;

public class AccountController : Controller
{
    // GET: /Account/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Sprawdzenie prawidłowości wpisanego hasła i loginu (tutaj można użyć logiki sprawdzającej zapis w bazie danych)
        if (username == "admin" && password == "admin")
        {
            HttpContext.Session.SetString("IsLoggedIn", "true"); // Zapisanie informacji o zalogowaniu w sesji
            return RedirectToAction("LoggedIn");
        }
        else
        {
            ViewBag.ErrorMessage = "Nieprawidłowy login lub hasło.";
            return View();
        }
    }

    // GET: /Account/LoggedIn
    public IActionResult LoggedIn()
    {
        // Sprawdzenie, czy użytkownik jest zalogowany na podstawie informacji w sesji
        if (HttpContext.Session.GetString("IsLoggedIn") == "true")
        {
            return View();
        }
        else
        {
            return RedirectToAction("Login");
        }
    }

    // GET: /Account/Logout
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("IsLoggedIn"); // Usunięcie informacji o zalogowaniu z sesji
        return RedirectToAction("Login");
    }
}

