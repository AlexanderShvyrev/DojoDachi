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
        public static DojoDachi dojoDachi = new DojoDachi ("https://s3-ap-southeast-1.amazonaws.com/images.marketing-interactive.com/wp-content/uploads/2018/11/14230247/Pokemon_McDonalds-Malaysia_1.jpeg", "Lets Play DojoDachi");
        [HttpGet ("")]
        public IActionResult Index () {
            ViewBag.Image = dojoDachi.ImageUrl;
            return View (dojoDachi);
        }

        [HttpGet ("feed")]
        public IActionResult Feed () {
            if (!dojoDachi.IsFull) {
                if (dojoDachi.Meals > 0) {
                    dojoDachi.Meals -= 1;
                    Random random = new Random ();
                    var amount = random.Next (5, 10);
                    dojoDachi.Fullness += amount;
                } else {
                    if (dojoDachi.Meals == 0) {
                        ViewBag.Message1 = "You do not have enought food";
                    }

                }
            }
            ViewBag.Image = "https://media1.giphy.com/media/Es3kyIm9VOsda/source.gif";
            ViewBag.Feed = dojoDachi.Fullness;
            ViewBag.Message = $"you spent {dojoDachi.Meals} to feed your Dojodachi he gained {dojoDachi.Fullness}";
            return View ("Index", dojoDachi);

        }


    [HttpGet ("play")]
    public IActionResult Play () {
        while (dojoDachi.Energy >= 5) {
            dojoDachi.Energy -= 5;
            Random random = new Random ();
            var mood = random.Next (5, 10);
            dojoDachi.Happiness += mood;
        }
        ViewBag.Image = "https://media.giphy.com/media/Mc9Xrw5mRs1cA/giphy.gif";
        ViewBag.Play = dojoDachi.Energy;
        ViewBag.Message = $"{dojoDachi.Name} got tired and lost {dojoDachi.Energy} energy.";
        return View ("Index", dojoDachi);
    }

    [HttpGet ("sleep")]
    public IActionResult Sleep () {
            dojoDachi.Energy+=15;
            dojoDachi.Fullness-=5;
            dojoDachi.Happiness-=5;
        ViewBag.Image="https://media.giphy.com/media/khMwtd5muCbK0/giphy.gif";
        ViewBag.Energy = dojoDachi.Energy;
        ViewBag.Message = $"{dojoDachi.Name} slept well and gained {dojoDachi.Energy} and lost {dojoDachi.Happiness} and {dojoDachi.Fullness}";
        return View ("Index", dojoDachi);
    }

    [HttpGet ("work")]
    public IActionResult Work () {
            dojoDachi.Energy-=5;
            var random=new Random();
            var dish=random.Next(1,4);
            dojoDachi.Meals+=dish;
        ViewBag.Image ="https://media3.giphy.com/media/472LylbvdqDgA/source.gif";
        ViewBag.Energy = dojoDachi.Energy;
        ViewBag.Meals = dojoDachi.Meals;
        ViewBag.Message = "Work hard or hardly working";
        return View ("Index", dojoDachi);
    }
    // [HttpGet ("dead")]
    // public IActionResult Dead () 
    // {
    //     get
    //     {
    //         if (dojoDachi.Fullness <= 0 || dojoDachi.Happiness <=0)
    //         {
                
    //             return true;
    //         }
    //         else
    //         {
    //             return false;
    //         }
    //     }
    //     return View ("Index");
    // }

}

}