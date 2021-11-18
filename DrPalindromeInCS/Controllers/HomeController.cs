using DrPalindromeInCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrPalindromeInCS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reverse()
        {
            Palindrome model = new();

            return View(model);
        }

        [HttpPost]
        // Ensure proper form is submitted/posted
        [ValidateAntiForgeryToken]
        // Create in IAR for Reverse Post and supply the Palindrome palindrome parameter
        public IActionResult Reverse(Palindrome palindrome)
        {
            string inputWord = palindrome.InputWord;
            string revWord = "";

            // Loop through input word from the end going forward, pushing each letter to revWord
            for (int i = inputWord.Length - 1; i >= 0; i--)
            {
                revWord += inputWord[i];
            }

            // Assign revWord to model palindrome.RevWord 
            palindrome.RevWord = revWord;

            // Use Regex.Replace() to remove special characters and spaces from revWord and inputWord for proper comparison
            revWord = Regex.Replace(revWord.ToLower(), "[^a-zA-Z0-9]+", "");
            inputWord = Regex.Replace(inputWord.ToLower(), "[^a-zA-Z0-9]+", "");

            // Check if user's inputWord == revWord, update model palindrome class (isPalindrome bool and Message) with results 
            if (revWord == inputWord)
            {
                palindrome.IsPalindrome = true;
                palindrome.Message = $"Success! --{palindrome.InputWord}-- is a palindrome";
            }
            else
            {
                palindrome.IsPalindrome = false;
                palindrome.Message = $"I'm sorry, --{palindrome.InputWord}-- is not a palindrome";
            }

            return View(palindrome);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
