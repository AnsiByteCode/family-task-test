using Domain.ClientSideModels;
using Domain.Commands;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions.ModelConversion
{
    /// <summary>
    /// Model Conversion Extensions
    /// </summary>
    public static class ModelConversionExtensions
    {
        /// <summary>
        /// Converts to createmembercommand.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static CreateMemberCommand ToCreateMemberCommand(this MemberVm model)
        {
            var command = new CreateMemberCommand()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Roles = model.Roles,
                Avatar = model.Avatar,
                Email = model.Email
            };
            return command;
        }

        /// <summary>
        /// Converts to menuitems.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns></returns>
        public static MenuItem[] ToMenuItems(this IEnumerable<MemberVm> models)
        {
            return models.Select(m => new MenuItem()
            {
                iconColor = m.Avatar,
                isActive = false,
                label = $"{m.LastName}, {m.FirstName}",
                referenceId = m.Id
            }).ToArray();
        }

        /// <summary>
        /// Converts to updatemembercommand.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static UpdateMemberCommand ToUpdateMemberCommand(this MemberVm model)
        {
            var command = new UpdateMemberCommand()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Roles = model.Roles,
                Avatar = model.Avatar,
                Email = model.Email
            };
            return command;
        }
    }
}
