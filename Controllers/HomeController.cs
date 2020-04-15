using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ComputerPet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerPet.Controllers {
    public class HomeController : Controller {
        public static DojoDachi dojoDachi = new DojoDachi ("https://i.redd.it/yvp807z7og0z.gif", "Lets Play DojoDachi");
        [HttpGet ("")]
        public IActionResult Index () {
            ViewBag.Image = dojoDachi.ImageUrl;
            return View (dojoDachi);
        }

        [HttpGet ("feed")]
        public IActionResult Feed () {
            if (dojoDachi.IsFull() == false && dojoDachi.Meals > 0) 
            {
                dojoDachi.Meals -= 1;
                Random random = new Random ();
                var amount = random.Next (5, 10);
                dojoDachi.Fullness += amount;
            
                if (dojoDachi.Meals == 0) 
                    {
                        ViewBag.Message1 = "You do not have enough food. You have to work to earn more meals";
                    }
            }
            else
            {
                ViewBag.Message1 = "I'm full!!!";
            }
            
            ViewBag.Image = "https://media1.giphy.com/media/Es3kyIm9VOsda/source.gif";
            ViewBag.Feed = dojoDachi.Fullness;
            ViewBag.Message = $"Pikachu really enjoined his food and he gained {dojoDachi.Fullness} points of fullness";
            return View ("Index", dojoDachi);

        }


    [HttpGet ("play")]
    public IActionResult Play () 
    {
        if (dojoDachi.Energy <= 0)
        {
            return View("Died", dojoDachi);
        }
        if (dojoDachi.YouWon() == true)
        {
            return View("Won", dojoDachi);
        }
        else
        {
            dojoDachi.Energy -= 5;
            Random random = new Random ();
            var mood = random.Next (5, 10);
            dojoDachi.Happiness += mood;
            ViewBag.Image = "https://media.giphy.com/media/Mc9Xrw5mRs1cA/giphy.gif";
            ViewBag.Play = dojoDachi.Energy;
            if (dojoDachi.Energy < 10)
            {
                ViewBag.Message1 = $"{dojoDachi.Name} is really tired and needs some rest";
            }
            return View ("Index", dojoDachi);
        }
        
    }

    [HttpGet ("sleep")]
    public IActionResult Sleep () {
        if (dojoDachi.Happiness <= 0 || dojoDachi.Fullness <=0)
        {
            return View("Died", dojoDachi);
        }
        if (dojoDachi.YouWon() == true)
        {
            return View("Won", dojoDachi);
        }
        else
        {
            dojoDachi.Energy+=15;
            dojoDachi.Fullness-=5;
            dojoDachi.Happiness-=5;
            ViewBag.Image="https://media.giphy.com/media/khMwtd5muCbK0/giphy.gif";
            ViewBag.Energy = dojoDachi.Energy;
            ViewBag.Message = $"{dojoDachi.Name} slept well and gained {dojoDachi.Energy} points of energy but lost {dojoDachi.Happiness} points of happiness and {dojoDachi.Fullness} points of fullness. You better watch out for that";
            return View ("Index", dojoDachi);
        }
        
    }

    [HttpGet ("work")]
    public IActionResult Work () 
    {
        if (dojoDachi.Energy <= 0)
        {
            return View("Died", dojoDachi);
        }
        if (dojoDachi.YouWon() == true)
        {
            return View("Won", dojoDachi);
        }
        else
        {
            dojoDachi.Energy-=5;
            var random=new Random();
            var dish=random.Next(1,4);
            dojoDachi.Meals+=dish;
            ViewBag.Image ="https://media3.giphy.com/media/472LylbvdqDgA/source.gif";
            ViewBag.Energy = dojoDachi.Energy;
            ViewBag.Meals = dojoDachi.Meals;
            if (dojoDachi.Energy < 10)
            {
                ViewBag.Message1 = $"{dojoDachi.Name} don't forget to have some rest please!!!";
            }
            return View ("Index", dojoDachi);
        }
        
    }
    [HttpGet ("dead")]
    public IActionResult Dead() 
    {
        dojoDachi.IsDead();
        if (dojoDachi.IsDead () == true) 
        {
            return View ("Died", dojoDachi);
        }
        else{
            return View("Index", dojoDachi);
        }
    }

    [HttpGet("won")]
    public IActionResult Won()
    {
        dojoDachi.YouWon();
        ViewBag.Congartulations="Congratulations!! You won!!!";
        return ViewBag.Congartulations;
    }

    [HttpGet("restart")]
    public IActionResult Restart()
    {
        dojoDachi.Restart();
        return View("Index", dojoDachi);
    }

}
}