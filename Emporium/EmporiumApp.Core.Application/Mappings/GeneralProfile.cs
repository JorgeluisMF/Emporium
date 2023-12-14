using AutoMapper;
using EmporiumApp.Core.Application.Dtos.Account;
using EmporiumApp.Core.Application.Dtos.Improvements;
using EmporiumApp.Core.Application.Dtos.Product;
using EmporiumApp.Core.Application.Dtos.ProductType;
using EmporiumApp.Core.Application.Dtos.SalesType;
using EmporiumApp.Core.Application.Dtos.UserAccounts;
using EmporiumApp.Core.Application.Features.Agents.Commands.ChangeStatus;
using EmporiumApp.Core.Application.Features.Improvements.Commands.CreateImprovement;
using EmporiumApp.Core.Application.Features.Improvements.Commands.UpdateImprovement;
using EmporiumApp.Core.Application.Features.ProductTypes.Commands.CreateProductType;
using EmporiumApp.Core.Application.Features.ProductTypes.Commands.UpdateProductType;
using EmporiumApp.Core.Application.Features.SalesType.Commands.CreateSaleType;
using EmporiumApp.Core.Application.Features.SalesType.Commands.UpdateSaleType;
using EmporiumApp.Core.Application.ViewModels.Carrito;
using EmporiumApp.Core.Application.ViewModels.Improvement;
using EmporiumApp.Core.Application.ViewModels.Product;
using EmporiumApp.Core.Application.ViewModels.ProductImprovement;
using EmporiumApp.Core.Application.ViewModels.ProductType;
using EmporiumApp.Core.Application.ViewModels.SalesType;
using EmporiumApp.Core.Application.ViewModels.User;
using EmporiumApp.Core.Domain.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {

            #region mappings

            #region User

            CreateMap<AuthenticationRequest, LoginViewModel>()

                .ReverseMap();

            CreateMap<AuthenticationResponse, UserSaveViewModel>()
                .ReverseMap();

            CreateMap<AuthenticationResponse, UserViewModel>()
                .ReverseMap();

            CreateMap<UpdateAgentViewModel, UserSaveViewModel>()
                .ReverseMap();

            CreateMap<RegisterBasicRequest, UserSaveViewModel>()

                .ReverseMap();

            CreateMap<ManagerSaveViewModel, UserSaveViewModel>()

                .ReverseMap();


            CreateMap<RegisterManagerRequest, RegisterManagerDevRequest>()
                .ReverseMap()
                .ForMember(x => x.IsVerified, opt => opt.Ignore());

            CreateMap<RegisterManagerRequest, ManagerSaveViewModel>()

                .ReverseMap();

            CreateMap<UpdateRequest, UserSaveViewModel>()

                .ReverseMap();

            CreateMap<UpdateRequest, ManagerSaveViewModel>()

                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()

                .ReverseMap();

            CreateMap<AgentDto, RegisterManagerRequest>()
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.IsVerified, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AgentDtoActive, AgentDto>()
                .ReverseMap()
                .ForMember(x => x.IsActive, opt => opt.Ignore());

            CreateMap<AgentDto, AuthenticationResponse>()
                .ForMember(x => x.RefreshToken, opt => opt.Ignore())
                .ForMember(x => x.JWToken, opt => opt.Ignore())
                .ForMember(x => x.TypeUser, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.ProfilePicture, opt => opt.Ignore())
                .ForMember(x => x.IsVerified, opt => opt.Ignore())
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AgentDtoActive, AuthenticationResponse>()
                .ForMember(x => x.RefreshToken, opt => opt.Ignore())
                .ForMember(x => x.JWToken, opt => opt.Ignore())
                .ForMember(x => x.TypeUser, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.ProfilePicture, opt => opt.Ignore())
                .ForMember(x => x.IsVerified, opt => opt.Ignore())
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());

            CreateMap<ChangeAgentStatusCommand, ChangeAgentStatusResponse>()
              .ReverseMap();

            CreateMap<UpdateResponse, ChangeAgentStatusResponse>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore());

            #endregion

            #region Improvement
            CreateMap<Improvement, ImprovementViewModel>()
                .ReverseMap();

            CreateMap<Improvement, ImprovementSaveViewModel>()
                .ReverseMap();

            CreateMap<Improvement, ImprovementDto>()
                .ReverseMap()
                .ForMember(x => x.Products, opt => opt.Ignore());

            CreateMap<UpdateImprovementCommand, ImprovementDto>()
               .ReverseMap();

            CreateMap<Improvement, CreateImprovementCommand>()
                .ReverseMap()
                .ForMember(x => x.Products, opt => opt.Ignore());

            CreateMap<Improvement, UpdateImprovementCommand>()
               .ReverseMap()
               .ForMember(x => x.Products, opt => opt.Ignore());

            CreateMap<Improvement, ImprovementUpdateResponse>()
               .ReverseMap()
               .ForMember(x => x.Products, opt => opt.Ignore());
            #endregion

            #region SaleType
            CreateMap<SaleType, SaleTypeViewModel>()
                .ReverseMap();

            CreateMap<SaleType, SaleTypeSaveViewModel>()
                .ReverseMap();

            CreateMap<SaleType, SalesTypeDto>()
              .ReverseMap();

            CreateMap<UpdateSaleTypeCommand, SalesTypeDto>()
                .ReverseMap();

            CreateMap<SaleType, CreateSaleTypeCommand>()
                .ReverseMap()
                .ForMember(x => x.Products, opt => opt.Ignore());

            CreateMap<SaleType, UpdateSaleTypeCommand>()
               .ReverseMap()
               .ForMember(x => x.Products, opt => opt.Ignore());

            CreateMap<SaleType, SaleTypeUpdateResponse>()
               .ReverseMap()
               .ForMember(x => x.Products, opt => opt.Ignore());

            #endregion

            #region ProductType
            CreateMap<ProductType, ProductTypeViewModel>()
                .ReverseMap();

            CreateMap<ProductType, ProductTypeSaveViewModel>()
                .ReverseMap();

            CreateMap<ProductType, ProductTypeDto>()
                .ReverseMap();

            CreateMap<UpdateProductTypeCommand, ProductTypeDto>()
                .ReverseMap();

            CreateMap<ProductType, CreateProductTypeCommand>()
                .ReverseMap()
                .ForMember(x => x.Products, opt => opt.Ignore());

            CreateMap<ProductType, UpdateProductTypeCommand>()
               .ReverseMap()
               .ForMember(x => x.Products, opt => opt.Ignore());

            CreateMap<ProductType, ProductTypeUpdateResponse>()
               .ReverseMap()
               .ForMember(x => x.Products, opt => opt.Ignore());


            #endregion

            #region product

            CreateMap<Product, ProductViewModel>()
              .ForMember(x => x.IsFavourite, opt => opt.Ignore())
              .ReverseMap()
              .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
              .ForMember(dest => dest.LastModified, opt => opt.Ignore())
              .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            CreateMap<Product, ProductSaveViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            CreateMap<Product, ProductDto>()
                .ForMember(x => x.ImprovementList, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.ProductType, opt => opt.Ignore())
                .ForMember(x => x.Improvements, opt => opt.Ignore())
                .ForMember(x => x.ProductImgUrl1, opt => opt.Ignore())
                .ForMember(x => x.ProductImgUrl2, opt => opt.Ignore())
                .ForMember(x => x.ProductImgUrl3, opt => opt.Ignore())
                .ForMember(x => x.ProductImgUrl4, opt => opt.Ignore());

            #endregion

            #region ProductCarrito

            CreateMap<Carrito, ProductCarritoViewModel>()
              .ReverseMap();

            CreateMap<Carrito, ProductCarritoSaveViewModel>()
                .ReverseMap();

            #endregion

            #region PropertyImproment

            CreateMap<ProductImprovement, ProductImprovementViewModel>()
             .ReverseMap()
             .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<ProductImprovement, ProductImprovementSaveViewModel>()
             .ReverseMap()
             .ForMember(x => x.Id, opt => opt.Ignore());

            #endregion

            #endregion
        }
    }
}
