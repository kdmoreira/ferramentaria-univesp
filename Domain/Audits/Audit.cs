using Domain.Models;
using System;
using System.Reflection;

namespace Domain.Audits
{
    public static class Audit
    {
        public static void Creation(BaseModel model, Guid usuarioLogadoID)
        {
            CreationProperties(model, usuarioLogadoID);
            foreach (PropertyInfo propertyInfo in model.GetType().GetProperties())
            {
                var baseType = propertyInfo.PropertyType.BaseType;
                if (baseType == typeof(BaseModel))
                {
                    var property = propertyInfo.GetValue(model) as BaseModel;
                    if (property != null)
                        if (property.ID == Guid.Empty)
                        {
                            Creation(property, usuarioLogadoID);
                            CreationProperties(property, usuarioLogadoID);
                        }
                }
            }
        }

        private static void CreationProperties(BaseModel model, Guid usuarioLogadoId)
        {
            model.ID = Guid.NewGuid();
            model.CriadoPor = usuarioLogadoId;
            model.CriadoEm = DateTime.UtcNow;
            model.Ativo = true;
        }

        public static void UpdateProperties(BaseModel model, BaseModel modelOld, Guid usuarioLogadoId)
        {
            model.CriadoEm = modelOld.CriadoEm;
            model.CriadoPor = modelOld.CriadoPor;
            model.AtualizadoPor = usuarioLogadoId;
            model.AtualizadoEm = DateTime.UtcNow;
        }
    }
}