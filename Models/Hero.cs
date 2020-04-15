using System;
using System.Collections.Generic;

namespace ComputerPet.Models
{
    public abstract class Hero
    {
        public string Name{get; set;}
        public int Happiness{get; set;}
        public int Fullness{get; set;}
        public int Energy{get; set;}
        public int Meals{get; set;}

        public string ImageUrl{get; set;}

        public string TagLine{get; set;}


        public Hero(string name, int happiness, int fullness, int energy, int meals, string imageurl, string tagline)
        {
            Name=name;
            Happiness=happiness;
            Fullness=fullness;
            Energy=energy;
            Meals=meals;
            ImageUrl=imageurl;
            TagLine=tagline;

        }

        // public abstract bool IsFull{get;}
        // public abstract bool IsDead{get;}
        public abstract void Feed();
        public abstract void Play();
        public abstract void Work();
        public abstract void Sleep();
    }
}