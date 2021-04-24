using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreApp.DAL.EF;
using StoreApp.DAL.Entities.ExtraTables;

namespace StoreApp.DAL.Repositories.ExtraTables
{
    public class SoldProductsRepository : BaseRepository<SoldProduct>
    {
        // Rating Settings
        // for example, a product that has been sold (sent to shops) 100 000 times has a 5 star rating
        readonly int fiveStarRating = 100_000;
        readonly int fourStarRating = 50_000;
        readonly int threeStarRating = 20_000;
        readonly int twoStarRating = 10_000;
        readonly int oneStarRating = 5_000;


        public SoldProductsRepository(StoreContext context) : base(context)
        {
        }

        public override async Task Update(SoldProduct entity)
        {
            //// получаем сущность из БД
            //var srchEntity = await Get(entity.Id);
            //// меняем сущность
            //srchEntity.Name = entity.Name;
            //// меняе ее статус 
            //db.Entry(srchEntity).State = System.Data.Entity.EntityState.Modified;
            //// сохраняем изменения
            //await db.SaveChangesAsync();
        }

        public int GetGeneralAmountProductSalesById(int id)
        {
            var result = Table.Where(p => p.ProductId == id).ToList();
            return GetGeneralAmountProductSales(result);
        }

        public int GetGeneralAmountProductSales(List<SoldProduct> selectedProducts)
        {
            int amount = 0;
            foreach (var product in selectedProducts)
            {
                amount += product.Amount;
            }

            return amount;
        }

        public int SetProductRating(int salesAmount)
        {
            if(salesAmount >= fiveStarRating)
            {
                return 5;
            }
            else if(salesAmount >= fourStarRating && salesAmount < fiveStarRating)
            {
                return 4;
            }
            else if (salesAmount >= threeStarRating && salesAmount < fourStarRating)
            {
                return 3;
            }
            else if (salesAmount >= twoStarRating && salesAmount < threeStarRating)
            {
                return 2;
            }
            else if (salesAmount >= oneStarRating && salesAmount < twoStarRating)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
