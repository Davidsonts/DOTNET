using System;
using System.Threading.Tasks;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly ProEventosContext context;
        public GeralPersist(ProEventosContext context)    
        {
            this.context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            this.context.Add(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            this.context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            this.context.Remove(entity);
        }
        public void DeleteRage<T>(T entityArray) where T : class
        {
            this.context.RemoveRange(entityArray);
        }
         
    }
}