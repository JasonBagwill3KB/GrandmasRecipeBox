﻿using RecipeBox.Data;
using RecipeBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBox.Services
{
    public class CommentService
    {
        private readonly Guid _userId;
        public CommentService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateComment(CommentCreate model)
        {
            var entity =
                new Comment()
                {
                    CommentId = model.CommentId,
                    Text = model.Text,
                    RecipeId = model.RecipeId,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CommentItem> GetComment()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Comments                    
                    .Select(
                        e =>
                        new CommentItem
                        {
                            CommentId = e.CommentId,
                            Text = e.Text,
                            RecipeId = e.Recipe.RecipeId,
                            CreatedUtc = e.CreatedUtc
                        }
                        );
                return query.ToArray();
            }
        }
        //public IEnumerable<RecipeCommentDetail> GetCommentsByRecipeId(int RecipeId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //            .Comments
        //            .Where(e => e.RecipeId == RecipeId && e.OwnerId == _userId)
        //            .Select(
        //                e =>
        //                new RecipeCommentDetail
        //                {
        //                    RecipeId = e.Recipe.RecipeId,
        //                    RecipeName = e.Recipe.RecipeName,
        //                    CommentId = e.CommentId,
        //                    Text = e.Text,
        //                    Instructions = e.Recipe.Instructions,
        //                    //Ingredients = e.Recipe.Ingredients,
        //                    TypeOfCuisine = e.Recipe.TypeOfCuisine,
        //                    TypeOfDish = e.Recipe.TypeOfDish
        //                }
        //                );
        //        return query.ToArray();
        //    }
        //}
        public CommentDetail GetCommentById(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.CommentId == commentId);
                return
                    new CommentDetail
                    {
                        CommentId = entity.CommentId,
                        Text = entity.Text,
                        RecipeId = entity.Recipe.RecipeId,
                        CreatedUtc = entity.CreatedUtc
                    };
            }
        }
        public bool UpdateComment(CommentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.CommentId == model.CommentId);

                entity.CommentId = model.CommentId;
                entity.Text = model.Text;
                entity.RecipeId = model.RecipeId;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            
            }
        }
        public bool DeleteComment(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Comments
                    .Single(e => e.CommentId == commentId);

                ctx.Comments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
