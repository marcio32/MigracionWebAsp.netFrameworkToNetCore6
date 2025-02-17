﻿using Common.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFinal.Data.Base
{
    public abstract class BaseManager<T> where T : class
    {
        #region Singleton Context
        private static ApplicationDbContext contextInstance = null;
  

        public BaseManager()
        {
            DbContextOptionsBuilder optionsBuilder;
        }
        public static ApplicationDbContext contextSingleton
        {
            get
            {
                if (contextInstance == null)
                    contextInstance = new ApplicationDbContext();

                return contextInstance;
            }
        }
    #endregion

    #region Abstracts Methods
    public abstract Task<List<T>> SearchListAsync(T entityModel);
        public abstract Task<T> SearchSingle(T entityModel);
        public abstract Task<bool> Delete(T entityModel);
        #endregion

        #region Public Methods
        public async Task<bool> Save(T entityModel, bool isNew)
        {
            try
            {

                // Valida si es un registro nuevo o existente
                if (isNew)
                    contextSingleton.Entry<T>(entityModel).State = EntityState.Added;
                else
                {
                    contextSingleton.Entry<T>(entityModel).State = EntityState.Modified;
                }



                // Valida si se guardaron los datos
                var result = await contextSingleton.SaveChangesAsync() > 0;
                contextSingleton.Entry(entityModel).State = EntityState.Detached;
                return result;
            }
            catch (Exception ex)
            {
                await LogHelper.LogError(ex, "Base_Manager", "Save");
                throw new ValidationException("Ha ocurrido un error al guardar un registro." + ex.Message);
            }
        }
        #endregion
    }
}
