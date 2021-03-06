﻿using System;
using RecipeBox.Data;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBox.Services
{
    public class RecipeService
    {
        private readonly Guid _userId;
        public RecipeService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateRecipe(RecipeCreate model)
        {
            var entity =
                new Recipe()
                {
                    OwnerId = _userId,
                    RecipeName = model.RecipeName,
                    Ingredients = model.Ingredients,
                    Instructions = model.Instructions,
                    SourceId = model.SourceId,
                    TypeOfCuisine = model.TypeOfCuisine,
                    TypeOfDish = model.TypeOfDish
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Recipes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<RecipeListItem> GetRecipes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Recipes
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new RecipeListItem
                        {
                            RecipeId = e.RecipeId,
                            RecipeName = e.RecipeName,
                            TypeOfCuisine = e.TypeOfCuisine,
                            TypeOfDish = e.TypeOfDish
                        }
                        );
                return query.ToArray();
            }
        }
        public RecipeDetails GetRecipeById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Recipes
                    .Single(e => e.RecipeId == id && e.OwnerId == _userId);
                return
                    new RecipeDetails
                    {
                        RecipeId = entity.RecipeId,
                        RecipeName = entity.RecipeName,
                        Ingredients = entity.Ingredients,
                        TypeOfCuisine = entity.TypeOfCuisine,
                        Instructions = entity.Instructions,
                        Comments = GetListOfCommentStrings(entity.Comments),
                        SourceName = entity.Source.SourceName,
                        SourceId = entity.Source.SourceId
                    };
                
            }
        }
        public List<string> GetListOfCommentStrings(List<Comment> comments)
        {
            List<string> commentText = new List<string>();

            foreach (Comment c in comments)
            {
                commentText.Add(c.Text);
            }
            return
                    commentText;
        }
        public IEnumerable<RecipeDetails> GetRecipesSourceId(int SourceId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Recipes
                    .Where(e => e.SourceId == SourceId && e.OwnerId == _userId)
                    .Select(
                            e =>
                            new RecipeDetails
                            {
                                RecipeId = e.RecipeId,
                                RecipeName = e.RecipeName,
                                Ingredients = e.Ingredients,
                                TypeOfCuisine = e.TypeOfCuisine,
                                TypeOfDish = e.TypeOfDish,
                                Instructions = e.Instructions,
                                SourceId = e.Source.SourceId,
                                SourceName = e.Source.SourceName
                            }
                            );
                return query.ToArray();
            }
        }
        public bool EditRecipe(RecipeEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Recipes
                    .Single(e => e.RecipeId == model.RecipeId && e.OwnerId == _userId);
                entity.RecipeName = model.RecipeName;
                entity.Ingredients = model.Ingredients;
                entity.TypeOfCuisine = model.TypeOfCuisine;
                entity.TypeOfDish = model.TypeOfDish;
                entity.Instructions = model.Instructions;
                entity.SourceId = model.SourceId;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteRecipe(int recipeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Recipes
                    .Single(e => e.RecipeId == recipeId && e.OwnerId == _userId);

                ctx.Recipes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
