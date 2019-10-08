using Core.Plugins.Extensions;
using DocumentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace DocumentService.Application.Components.Impl
{
    public class ParserComponent : IParserComponent
    {
        private const string _delimeter = ":";

        public TemplateMetadataEntity ParseTemplate(string template)
        {
            var templateMetadataEntity = new TemplateMetadataEntity();

            List<string> templateTokens = GetTokensFromTemplate(template);

            foreach (string viewName in GetAllViewsFromTokens(templateTokens))
            {
                var tokenMapEntity = new TokenMapEntity
                {
                    ViewName = viewName
                };

                foreach (string token in templateTokens.Where(s => s.StartsWith("{{" + viewName)))
                {
                    string fieldName = null;
                    string tokenKey = token.ToString().Remove("{").Remove("}");

                    if (tokenKey != null && tokenKey.Contains(_delimeter))
                    {
                        fieldName = tokenKey.SubstringAfter(_delimeter);
                    }

                    tokenMapEntity.TokenEntities.Add(
                        new TokenEntity
                        {
                            TokenRaw = token,
                            TokenKey = tokenKey,
                            FieldName = fieldName
                        });
                }

                templateMetadataEntity.TokenMapEntities.Add(tokenMapEntity);
            }

            return templateMetadataEntity;
        }

        public Dictionary<string, string> CreateDynamicMap(TokenMapEntity tokenMapEntity, DataRow dataRow)
        {
            var dynamicMap = new Dictionary<string, string>();

            foreach (TokenEntity tokenEntity in tokenMapEntity.TokenEntities)
            {
                string fieldValue = GetStringFromDataRow(dataRow, tokenEntity.FieldName);

                dynamicMap.Add(tokenEntity.TokenKey, fieldValue);
            }

            return dynamicMap;
        }

        #region Private

        public List<string> GetTokensFromTemplate(string template)
        {
            var regex = new Regex(@"{{(.*?)}}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return regex.Matches(template).Select(match => match.ToString()).OrderBy(s => s).ToList();
        }

        public List<string> GetAllViewsFromTokens(List<string> templateTokens)
        {
            return templateTokens.Select(s => s.Remove("{").SubstringBefore(_delimeter)).Distinct().OrderBy(s => s).ToList();
        }

        private string GetStringFromDataRow(DataRow dataRow, string fieldName)
        {
            if (!dataRow.Table.Columns.Contains(fieldName) || dataRow[fieldName] == DBNull.Value)
            {
                return string.Empty;
            }

            return dataRow[fieldName].ToString();
        }

        #endregion
    }
}
