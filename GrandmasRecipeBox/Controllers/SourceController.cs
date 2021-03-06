﻿using Microsoft.AspNet.Identity;
using RecipeBox.Models;
using RecipeBox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GrandmasRecipeBox.Controllers
{
    [Authorize]
    public class SourceController : ApiController
    {
        private SourceService CreateSourceService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var sourceService = new SourceService(userId);
            return sourceService; 
        }
        /// <summary>
        /// The user can get a list of all sources
        /// </summary>
        /// <returns>The list of sources</returns>
        public IHttpActionResult Get()
        {
            SourceService sourceService = CreateSourceService();
            var sources = sourceService.GetSources();
            return Ok(sources);
        }
        /// <summary>
        /// This allows the user to enter a source for the recipe
        /// </summary>
        /// <param name="source"></param>
        /// <returns>200 Ok</returns>
        public IHttpActionResult Post(SourceCreate source)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSourceService();

            if (!service.CreateSource(source))
                return InternalServerError();

            return Ok();
        }
        /// <summary>
        /// This gives the source by sourceId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The specified source</returns>
        public IHttpActionResult Get(int id)
        {
            SourceService sourceService = CreateSourceService();
            var source = sourceService.GetSourceById(id);
            return Ok(source);
        }
        /// <summary>
        /// You can update the information on a source
        /// </summary>
        /// <param name="source"></param>
        /// <returns>200 ok</returns>
        public IHttpActionResult Put(SourceEdit source)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSourceService();

            if (!service.UpdateSource(source))
                return InternalServerError();

            return Ok();
        }

        /*public IHttpActionResult Delete(int id)
        {
            var service = CreateSourceService();

            if (!service.DeleteSource(id))
                return InternalServerError();

            return Ok();
        }*/
    }
}
