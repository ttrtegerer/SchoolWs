﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Леарн.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=DbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Session_Client { get; set; }
        public virtual DbSet<ClientService> Session_ClientService { get; set; }
        public virtual DbSet<DocumentByService> Session_DocumentByService { get; set; }
        public virtual DbSet<Gender> Session_Gender { get; set; }
        public virtual DbSet<Manufacturer> Session_Manufacturer { get; set; }
        public virtual DbSet<Product> Session_Product { get; set; }
        public virtual DbSet<ProductPhoto> Session_ProductPhoto { get; set; }
        public virtual DbSet<ProductSale> Session_ProductSale { get; set; }
        public virtual DbSet<ServicePhoto> Session_ServicePhoto { get; set; }
        public virtual DbSet<Tag> Session_Tag { get; set; }
        public virtual DbSet<Service> Session_Service { get; set; }
    }
}
