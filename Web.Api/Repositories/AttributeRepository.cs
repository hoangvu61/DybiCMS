using Microsoft.EntityFrameworkCore;
using Polly;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.WebSockets;
using Web.Api.Data;
using Web.Api.Entities;
using Web.Models;

namespace Web.Api.Repositories
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly WebDbContext _context;
        public AttributeRepository(WebDbContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Attribute>> GetAttributes(Guid companyId)
        {
            var query = _context.Attributes.Include(e => e.AttributeLanguages).Where(e => e.CompanyId == companyId);
            return await query.ToListAsync();
        }
        public async Task<List<AttributeLanguage>> GetAttributes(Guid companyId, string languageCode)
        {
            var query = _context.AttributeLanguages.Where(e => e.Attribute.CompanyId == companyId && e.LanguageCode == languageCode);
            return await query.ToListAsync();
        }
        public async Task<List<AttributeLanguage>> GetAttributes(Guid companyId, string languageCode, List<Guid> categoryIds)
        {

            var query = _context.AttributeLanguages
                .Include(e => e.Attribute)
                .Where(e => e.Attribute.CompanyId == companyId && e.LanguageCode == languageCode && e.Attribute.AttributeCategories.Any(a => categoryIds.Contains(a.CategoryId)));
            return await query.ToListAsync();
        }
        public async Task<Entities.Attribute> GetAttribute(Guid companyId, string id)
        {
            var query = _context.Attributes.Include(e => e.AttributeLanguages).Where(e => e.Id == id && e.CompanyId == companyId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<Entities.Attribute> CreateAttribute(Entities.Attribute attribute)
        {
            _context.Attributes.Add(attribute);
            await _context.SaveChangesAsync();
            return attribute;
        }
        public async Task<Entities.Attribute> UpdateAttribute(Entities.Attribute attribute)
        {
            _context.Attributes.Update(attribute);
            await _context.SaveChangesAsync();
            return attribute;
        }
        public async Task<bool> DeleteAttribute(Entities.Attribute attribute)
        {
            var attributeCategories = await _context.AttributeCategories.Where(e => e.AttributeId == attribute.Id && e.CompanyId == attribute.CompanyId).ToArrayAsync();
            _context.AttributeCategories.RemoveRange(attributeCategories);

            var attributes = await _context.ItemAttributes.Where(e => e.AttributeId == attribute.Id && e.Item.CompanyId == attribute.CompanyId).ToArrayAsync();
            _context.ItemAttributes.RemoveRange(attributes);

            _context.Attributes.Remove(attribute);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AttributeSource>> GetAttributeSources(Guid companyId)
        {
            var query = _context.AttributeSources.Include(e => e.AttributeSourceLanguages).Where(e => e.CompanyId == companyId);
            return await query.ToListAsync();
        }
        public async Task<List<AttributeSourceLanguage>> GetAttributeSources(Guid companyId, string languageCode)
        {
            var query = _context.AttributeSourceLanguages.Where(e => e.Source.CompanyId == companyId && e.LanguageCode == languageCode);
            return await query.OrderBy(e => e.Name).ToListAsync();
        }
        public async Task<AttributeSource> GetAttributeSource(Guid companyId, Guid id)
        {
            var query = _context.AttributeSources.Include(e => e.AttributeSourceLanguages).Where(e => e.Id == id && e.CompanyId == companyId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<AttributeSource> CreateAttributeSource(AttributeSource source)
        {
            _context.AttributeSources.Add(source);
            await _context.SaveChangesAsync();
            return source;
        }
        public async Task<AttributeSource> UpdateAttributeSource(AttributeSource source)
        {
            _context.AttributeSources.Update(source);
            await _context.SaveChangesAsync();
            return source;
        }
        public async Task<bool> DeleteAttributeSource(AttributeSource source)
        {
            var values = await _context.AttributeValues.Where(e => e.SourceId == source.Id).ToArrayAsync();
            _context.AttributeValues.RemoveRange(values); 
            _context.AttributeSources.Remove(source);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AttributeValue>> GetAttributeValues(Guid companyId)
        {
            var query = _context.AttributeValues.Include(e => e.AttributeValueLanguages).Where(e => e.Source.CompanyId == companyId);
            return await query.OrderBy(e => e.SourceId).ToListAsync();
        }

        public async Task<List<AttributeValueLanguage>> GetAttributeValues(Guid companyId, string language, List<Guid> sourceids)
        {
            var query = _context.AttributeValueLanguages.Include(e => e.AttributeValue)
                .Where(e => e.AttributeValue.Source.CompanyId == companyId && e.LanguageCode == language && sourceids.Contains(e.AttributeValue.SourceId));
            return await query.ToListAsync();
        }
        public async Task<AttributeValue> GetAttributeValue(Guid companyId, string id)
        {
            var query = _context.AttributeValues.Include(e => e.AttributeValueLanguages).Where(e => e.Id == id && e.Source.CompanyId == companyId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<AttributeValue> CreateAttributeValue(AttributeValue value)
        {
            _context.AttributeValues.Add(value);
            await _context.SaveChangesAsync();
            return value;
        }
        public async Task<AttributeValue> UpdateAttributeValue(AttributeValue value)
        {
            _context.AttributeValues.Update(value);
            await _context.SaveChangesAsync();
            return value;
        }
        public async Task<bool> DeleteAttributeValue(AttributeValue value)
        {
            _context.AttributeValues.Remove(value);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AttributeSourceLanguage>> GetAttributeSourceLanguages(Guid companyId, List<Guid> ids)
        {
            var objs = await _context.AttributeSourceLanguages.Where(e => e.Source.CompanyId == companyId && ids.Contains(e.AttributeSourceId)).ToListAsync();
            return objs;
        }
        public async Task<AttributeSourceLanguage> GetAttributeSourceLanguage(Guid companyId, Guid id, string languageCode)
        {
            var obj = await _context.AttributeSourceLanguages.Where(e => e.Source.CompanyId == companyId && e.AttributeSourceId == id && e.LanguageCode == languageCode).FirstOrDefaultAsync();
            return obj;
        }
        public async Task<AttributeSourceLanguage> CreateAttributeSourceLanguage(AttributeSourceLanguage lang)
        {
            _context.AttributeSourceLanguages.Add(lang);
            await _context.SaveChangesAsync();
            return lang;
        }
        public async Task<AttributeSourceLanguage> UpdateAttributeSourceLanguage(AttributeSourceLanguage lang)
        {
            _context.AttributeSourceLanguages.Update(lang);
            await _context.SaveChangesAsync();
            return lang;
        }
        public async Task<bool> DeleteAttributeSourceLanguage(AttributeSourceLanguage lang)
        {
            _context.AttributeSourceLanguages.Remove(lang);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AttributeCategory>> GetAttributeCategories(Guid companyId)
        {
            var query = _context.AttributeCategories
                .Include(e => e.Attribute)
                .Include(e => e.Attribute.AttributeLanguages)
                .Include(e => e.Category)
                .Include(e => e.Category.Item)
                .Include(e => e.Category.Item.ItemLanguages)
                .Where(e => e.CompanyId == companyId);
            return await query.OrderBy(e => e.CategoryId).ThenBy(e => e.Order).ToListAsync();
        }
        public async Task<AttributeCategory> GetAttributeCategory(Guid companyId, string attributeId, Guid categoryId)
        {
            var query = _context.AttributeCategories.Include(e => e.Attribute).Where(e => e.AttributeId == attributeId && e.CategoryId == categoryId && e.CompanyId == companyId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<AttributeCategory> CreateAttributeCategory(AttributeCategory attCat)
        {
            _context.AttributeCategories.Add(attCat);
            await _context.SaveChangesAsync();
            return attCat;
        }
        public async Task<AttributeCategory> UpdateAttributeCategory(AttributeCategory attCat)
        {
            _context.AttributeCategories.Update(attCat);
            await _context.SaveChangesAsync();
            return attCat;
        }
        public async Task<bool> DeleteAttributeCategory(AttributeCategory attCat)
        {
            var category = await _context.ItemCategories.FirstOrDefaultAsync(e => e.ItemId == attCat.CategoryId && e.Item.CompanyId == attCat.CompanyId);
            if (category != null)
            {
                switch (category.Type)
                {
                    case "ART":
                        var itemArrticleAttributes = await _context.ItemArticles.Where(e => e.CategoryId == attCat.CategoryId && e.Item.CompanyId == attCat.CompanyId
                                                                            && e.Item.Attributes.Any(a => a.AttributeId == attCat.AttributeId))
                            .Select(e => e.Item.Attributes.Where(a => a.AttributeId == attCat.AttributeId))
                            .FirstOrDefaultAsync();
                        if (itemArrticleAttributes != null) _context.ItemAttributes.RemoveRange(itemArrticleAttributes);
                        break;
                    case "PRO":
                        var itemProductAttributes = await _context.ItemProducts.Where(e => e.CategoryId == attCat.CategoryId && e.Item.CompanyId == attCat.CompanyId
                                                                            && e.Item.Attributes.Any(a => a.AttributeId == attCat.AttributeId))
                            .Select(e => e.Item.Attributes.Where(a => a.AttributeId == attCat.AttributeId))
                            .FirstOrDefaultAsync();
                        if (itemProductAttributes != null) _context.ItemAttributes.RemoveRange(itemProductAttributes);
                        break;
                    case "MID":
                        var itemMediaAttributes = await _context.ItemMedias.Where(e => e.CategoryId == attCat.CategoryId && e.Item.CompanyId == attCat.CompanyId
                                                                            && e.Item.Attributes.Any(a => a.AttributeId == attCat.AttributeId))
                            .Select(e => e.Item.Attributes.Where(a => a.AttributeId == attCat.AttributeId))
                            .FirstOrDefaultAsync();
                        if (itemMediaAttributes != null) _context.ItemAttributes.RemoveRange(itemMediaAttributes);
                        break;
                }    
            }
            _context.AttributeCategories.Remove(attCat);
            await _context.SaveChangesAsync();
            return true;
        }

        #region AttributeOrder
        public async Task<List<AttributeOrder>> GetAttributeOrders(Guid companyId)
        {
            var query = _context.AttributeOrders
                .Include(e => e.Attribute)
                .Include(e => e.Attribute.AttributeLanguages)
                .Where(e => e.CompanyId == companyId);
            return await query.OrderBy(e => e.Order).ToListAsync();
        }
        public async Task<AttributeOrder> GetAttributeOrder(Guid companyId, string attributeId)
        {
            var query = _context.AttributeOrders
                .Include(e => e.Attribute)
                .Where(e => e.AttributeId == attributeId && e.CompanyId == companyId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<AttributeOrder> CreateAttributeOrder(AttributeOrder attOrder)
        {
            _context.AttributeOrders.Add(attOrder);
            await _context.SaveChangesAsync();
            return attOrder;
        }
        public async Task<AttributeOrder> UpdateAttributeOrder(AttributeOrder attOrder)
        {
            _context.AttributeOrders.Update(attOrder);
            await _context.SaveChangesAsync();
            return attOrder;
        }
        public async Task<bool> DeleteAttributeOrder(AttributeOrder attOrder)
        {
            _context.AttributeOrders.Remove(attOrder);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region AttributeContact
        public async Task<List<AttributeContact>> GetAttributeContacts(Guid companyId)
        {
            var query = _context.AttributeContacts
                .Include(e => e.Attribute)
                .Include(e => e.Attribute.AttributeLanguages)
                .Where(e => e.CompanyId == companyId);
            return await query.OrderBy(e => e.Order).ToListAsync();
        }
        public async Task<AttributeContact> GetAttributeContact(Guid companyId, string attributeId)
        {
            var query = _context.AttributeContacts
                .Include(e => e.Attribute)
                .Where(e => e.AttributeId == attributeId && e.CompanyId == companyId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<AttributeContact> CreateAttributeContact(AttributeContact attContact)
        {
            _context.AttributeContacts.Add(attContact);
            await _context.SaveChangesAsync();
            return attContact;
        }
        public async Task<AttributeContact> UpdateAttributeContact(AttributeContact attContact)
        {
            _context.AttributeContacts.Update(attContact);
            await _context.SaveChangesAsync();
            return attContact;
        }
        public async Task<bool> DeleteAttributeContact(AttributeContact attContact)
        {
            _context.AttributeContacts.Remove(attContact);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
