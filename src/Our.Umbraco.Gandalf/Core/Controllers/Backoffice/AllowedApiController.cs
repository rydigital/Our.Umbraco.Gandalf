using Our.Umbraco.Gandalf.Core.Models;
using Our.Umbraco.Gandalf.Core.Constants;
using Our.Umbraco.Gandalf.Core.Models.DTOs;
using Our.Umbraco.Gandalf.Core.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Our.Umbraco.Gandalf.Controllers.Backoffice
{
    [PluginController("Gandalf")]
    public class AllowedIpApiController : UmbracoAuthorizedApiController
    {
        private IAllowedIpService _allowedIpService;

        public AllowedIpApiController(IAllowedIpService allowedIpService)
        {
            _allowedIpService = allowedIpService;
        }


        [HttpGet]
        public IEnumerable<AllowedIpDto> GetAll()
        {
            return _allowedIpService.GetAll();
        }

        [HttpPost]
        public AddResponse Add(AddRequest request)
        {
            if (request == null) return new AddResponse() { Success = false, Message = "Request was empty" };
            if (!ModelState.IsValid) return new AddResponse() { Success = false, Message = "Missing required attributes" };

            try
            {
                var item = _allowedIpService.Create(request.ipAddress, request.Notes);
                return new AddResponse() { Success = true, Item = item };
            }
            catch(Exception e)
            {
                return new AddResponse() { Success = false, Message = "There was an error adding the item : " + e.Message };
            }
            
        }

        [HttpPost]
        public UpdateResponse Update(UpdateRequest request)
        {

            if (request == null) return new UpdateResponse() { Success = false, Message = "Request was empty" };
            if (!ModelState.IsValid) return new UpdateResponse() { Success = false, Message = "Missing required attributes" };

            try
            {
                var item = _allowedIpService.Update(request.Item);
                return new UpdateResponse() { Success = true, Item = item };
            }
            catch (Exception e)
            {
                return new UpdateResponse() { Success = false, Message = "There was an error updating the item : "+e.Message };
            }
        }

        [HttpDelete]
        public DeleteResponse Delete(int id)
        {
            if (id == 0) return new DeleteResponse() { Success = false, Message = "Invalid ID passed for item to delete" };

            try
            {
                _allowedIpService.Delete(id);
                return new DeleteResponse() { Success = true };
            }
            catch(Exception e)
            {
                return new DeleteResponse() { Success = false, Message = "There was an error deleting the item : " + e.Message };
            }
        }

        [HttpPost]
        public void ClearCache()
        {
			throw new Exception("Not sure if we should implement this");
        }

		[HttpPost]
		public UpdateResponse ToggleStatus(UpdateStatus model)
		{
		
			try
			{
				var item = _allowedIpService.UpdateAppStatus(GandalfConstants.Key, model.CurrentStatus.ToString());
				return new UpdateResponse() { Success = true, Message = "Status successfully updated" };
			}
			catch (Exception e)
			{
				return new UpdateResponse() { Success = false, Message = "There was an error updating the status" + e.Message };
			}

		}
	}
}
