﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBox.Data
{
    public class Recipe
    {

        [Key]
        public int RecipeId { get; set; }

        [Required]
<<<<<<< HEAD
<<<<<<< HEAD
        public string RecipeName { get; set; }

=======
        public string RecipeName { get; set; }       
>>>>>>> 0a4aae2383ef581ca34cb9487a402295641c7367
=======
        public string RecipeName { get; set; }


        [ForeignKey(nameof(RecipeContent))]
        public int RecipeContentId { get; set; }
        public virtual RecipeContent RecipeContent { get; set; }
>>>>>>> fd42b2e23e2d43a6eea1d1571c6ac7c799009248


        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string Instructions { get; set; }

        [ForeignKey(nameof(Source))]
        public int SourceId { get; set; }

        public virtual Source Source { get; set; }

        public CuisineCategory TypeOfCuisine { get; set; }

        public DishType TypeOfDish { get; set; }
    }
    public enum CuisineCategory
    {
        African = 1,
        American,
        Asian,
        Australian,
        Austrian,
        Belgian,
        Brazilian,
        British,
        Cajun,
        Carribean,
        Central_American,
        Chinese,
        Creole,
        Cuban,
        Eastern_European,
        Filipino,
        French,
        Greek,
        Icelandic,
        Indian,
        Indonesian,
        Irish,
        Italian,
        Japanese,
        Jewish,
        Korean,
        Latin_American,
        Malaysian,
        Mediterranean,
        Mexican,
        Middle_Eastern,
        Moroccan,
        New_England,
        Pakistani,
        Portuguese,
        Provencal,
        Russian,
        Scandinavian,
        South_American,
        Southern,
        Southwestern,
        Spanish,
        Thai,
        Tibetan,
        Turkish,
        Vietnamese
    }
    public enum DishType
    {
        Salad = 1,
        Side,
        Main,
        Dessert,
        Soup,
        Appetizer



    }
}
