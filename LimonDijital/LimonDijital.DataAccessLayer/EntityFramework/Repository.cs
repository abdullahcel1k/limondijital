using LimonDijital.Common;
using LimonDijital.Core.DataAccess;
using LimonDijital.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LimonDijital.DataAccessLayer.EntityFramework
{
	// Where cümlesi ile kısıtlama yapıyoruz yani sadece sınıf nesneleri ile çalışıyoruz
	// int string girilmesini önlüyoruz

	public class Repository<T> : RepositoryBase, IDataAccess<T> where T : class
	{
		// bu değişkeni ve constructorda çağrılmasını bir adım ileriye taşıyarak repositorybase sınıfını miras aldık
		// bu sayede onun protected değişkenlerini kullanabilieceğiz ve dbcontext oluşumu o sınıf üzerinden gerçekleşicek
		//private DatabaseContext db;
		/* gelen objeyi _objectSet adlı değişkende sklayıp ilgili işlemlerde ona göre işle yapıyoruz*/
		private DbSet<T> _objectSet;

		public Repository()
		{
			// ilişkisel bir tabloya veri eklerken bir den fazla database context oluşturmayı önlemeliyiz
			// db = RepositoryBase.CreateContext();

			_objectSet = context.Set<T>();
		}


		public List<T> List()
		{
			return _objectSet.ToList();
		}

		// bu şekilde kullanıcının group by vs gibi şekillendirebilieceği nesneler dönderebiliriz
		//public IQueryable<T> List(Expression<Func<T,bool>> where)
		//{
		//	return _objectSet.Where(where);
		//}

		public IQueryable<T> ListQueryable()
		{
			return _objectSet.AsQueryable<T>();
		}
		public List<T> List(Expression<Func<T, bool>> where)
		{
			return _objectSet.Where(where).ToList();
		}

		public int Insert(T obj)
		{
			_objectSet.Add(obj);

			if (obj is MyEntityBase)
			{
				MyEntityBase o = obj as MyEntityBase; // gelen nesneyi MyEntityBase classına çevirdik cast ettik
				DateTime now = DateTime.Now;

				o.CreatedOn = now;
				o.ModifiedOn = now;
				o.ModifiedUsername = App.Common.GetCurrentUsername(); // TODO : işlem yapan kullanıcı adı yazılmalı
			}

			return Save();
		}

		public int Update(T obj)
		{
			if (obj is MyEntityBase)
			{
				MyEntityBase o = obj as MyEntityBase; // gelen nesneyi MyEntityBase classına çevirdik cast ettik

				o.ModifiedOn = DateTime.Now;
				o.ModifiedUsername = App.Common.GetCurrentUsername(); // TODO : işlem yapan kullanıcı adı yazılmalı
			}

			return Save();
		}

		public int Delete(T obj)
		{
			// eğer tamamaen dbden silinmeyip onu sadece silindi değerini true yaparak takip etmek istersek kayıt ediyoruz
			//if (obj is MyEntityBase)
			//{
			//	MyEntityBase o = obj as MyEntityBase; // gelen nesneyi MyEntityBase classına çevirdik cast ettik

			//	o.ModifiedOn = DateTime.Now;
			//	o.ModifiedUsername = App.Common.GetCurrentUsername(); // TODO : işlem yapan kullanıcı adı yazılmalı
			//}

			_objectSet.Remove(obj);
			return Save();
		}

		public int Save()
		{
			return context.SaveChanges();
		}

		public T Find(Expression<Func<T, bool>> where)
		{
			return _objectSet.FirstOrDefault(where);
		}
	}
}
