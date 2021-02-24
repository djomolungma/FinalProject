using Castle.DynamicProxy;
using Core.CrossCuttingConserns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception//Aspect
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //defensive coding - savunma odaklı kodlama
            if (!typeof(IValidator).IsAssignableFrom(validatorType))// validatorType bir IValidator mu ?
            {
                throw new System.Exception("Bu bir doğrulama sınıfı degildir");//throw new System.Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//Reflaction (çalışma anında birşeyleri çalıştırabilmemizi sağlar yani instance oluşturur) yani New'leme yapar çalışma esnasında //ValidatorType ProductValidator
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];// oluşturulan instance nin tipini bulur //Abstract validatorun generik argumanlarından 0 ıncısının tipini yakala örn Product'un tipi
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);// oluşturulan instance ilgili methodunun parametlelerini bul //Methodun argümanlarını gez tipi Product ise onu validate et
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
