using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Data;
using Amazon.DynamoDB;
using Amazon.DynamoDB.Model;

namespace EixoX.Amazon
{
    public class DynamoDbEngine
        : EixoX.Data.ClassStorageEngine
    {
        private readonly AmazonDynamoDBClient _Client;

        private void AppendFilter(Dictionary<string, ExpectedAttributeValue> source, Data.ClassFilter filter, bool onlyAndOperation)
        {

        }

        public int Delete(Data.DataAspect aspect, Data.ClassFilter filter)
        {
            DeleteItemRequest request = new DeleteItemRequest();
            request.TableName = aspect.StoredName;


            if (filter != null)
                AppendFilter(request.Expected, filter, true);

            DeleteItemResponse response = _Client.DeleteItem(request);

            return response.DeleteItemResult.ConsumedCapacityUnits > 0 ?
                1 : 0;

        }

        public int Insert(Data.DataAspect aspect, IEnumerable<AspectMemberValue> values, out object identityValue)
        {
            identityValue = null;
            return 0;

            PutItemRequest request = new PutItemRequest();
            foreach (AspectMemberValue amv in values)
            {
                ExpectedAttributeValue eav = new ExpectedAttributeValue();
                eav.Value = new AttributeValue();
                request.Expected.Add(aspect[amv.Ordinal].StoredName, eav);
            }

        }

        public int Insert(Data.DataAspect aspect, System.Collections.IEnumerable entities)
        {
            throw new NotImplementedException();
        }

        public int Update(Data.DataAspect aspect, IEnumerable<AspectMemberValue> values, Data.ClassFilter filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Select<T>(Data.DataAspect aspect, Data.ClassFilter filter, Data.ClassSort sort, int pageSize, int pageOrdinal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> SelectMember(Data.DataAspect aspect, int ordinal, Data.ClassFilter filter, Data.ClassSort sort, int pageSize, int pageOrdinal)
        {
            throw new NotImplementedException();
        }

        public object GetMemberValue(Data.DataAspect aspect, int ordinal, Data.ClassFilter filter)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Data.DataAspect aspect, Data.ClassFilter filter)
        {
            throw new NotImplementedException();
        }

        public long Count(Data.DataAspect aspect, Data.ClassFilter filter)
        {
            throw new NotImplementedException();
        }

        public Data.ClassFilter CreateSearchFilter(Data.DataAspect aspect, string filter)
        {
            throw new NotImplementedException();
        }
    }
}
