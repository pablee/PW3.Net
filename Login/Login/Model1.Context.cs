﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Login
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TP_Entities : DbContext
    {
        public TP_Entities()
            : base("name=TP_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ArchivoTarea> ArchivoTareas { get; set; }
        public virtual DbSet<Carpeta> Carpetas { get; set; }
        public virtual DbSet<ComentarioTarea> ComentarioTareas { get; set; }
        public virtual DbSet<Tarea> Tareas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
    }
}
