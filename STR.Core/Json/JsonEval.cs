using Irony.Parsing;
using Newtonsoft.Json.Linq;
using STR.Core.Contants;
using STR.Core.Exceptions;
using STR.Core.Identifiers;

namespace STR.Core
{
    public class JsonEval : NodeEvalBase
    {
        public JsonContainer ReadNode(ParseTreeNode node, JsonContainer rootObj)
        {
            // 获取查询对象
            var nestedNode = GetNestedNode(node);
            if (nestedNode != null)
            {
                if (nestedNode.Term.Name != TermConstants.DataSource)
                    rootObj = ReadNode(nestedNode, rootObj);
            }

            if (node.Term.Name == TermConstants.SelectStatement)
            {
                var subNode = GetNestedNode(node);
                if (subNode != null)
                {
                    var propertyNode = node.ChildNodes[1].ChildNodes[0];
                    if (rootObj.Value.Type == JTokenType.Array)
                    {
                        JArray jArray = new JArray();
                        var arr = (JArray)rootObj.Value;
                        foreach (var item in arr)
                        {
                            jArray.Add(SetModelFromNode(propertyNode, new JsonContainer(item)).Value);
                        }
                        return new JsonContainer(jArray);
                    }
                    else
                    {
                        return SetModelFromNode(propertyNode, rootObj);
                    }
                }
            }
            return rootObj;
        }

        private JsonContainer SetModelFromNode(ParseTreeNode propertyNode, JsonContainer data)
        {
            JObject keyValuePairs = new JObject();
            foreach (var identifier in propertyNode.ChildNodes)
            {
                var identity = GetIdentifierModel(identifier);

                var sectionData = SetModelFromFunctionModel(data, identity);
                if (sectionData is JsonContainer json)
                    keyValuePairs.Add(identity.GetFirstIdentifierName(), json.Value);
                else if (sectionData is StringContainer str)
                    keyValuePairs.Add(identity.GetFirstIdentifierName(), str.Value);
            }
            if (propertyNode.ChildNodes.Count == 1)
            {
                var firstIdentifier = GetIdentifierModel(propertyNode.ChildNodes[0]);
                return new JsonContainer(keyValuePairs[firstIdentifier.GetFirstIdentifierName()]);
            }
            return new JsonContainer(keyValuePairs);
        }

        private Container SetModelFromFunctionModel(JsonContainer data, BaseIdentifierModel identity)
        {
            if (identity.CurrentType == IdentityType.Identity)
                return new JsonContainer(data.Value[identity.IdentifierName]);
            else
            {
                var method = identity as FunctionIdentityModel<StringContainer, StringContainer>;
                Container identityValue;
                if (method.Child != null)
                {
                    identityValue = SetModelFromFunctionModel(data, method.Child);
                }
                else
                {
                    identityValue = new JsonContainer(data.Value[identity.IdentifierName]);
                }
                if (identityValue is JsonContainer d)
                {
                    if (d.Value.Type == JTokenType.String)
                        return method.Evaluate(new StringContainer(d.Value.Value<string>()));
                    else
                        throw new STRIdentifierNotFoundException(identity.IdentifierName);
                }
                else if (identityValue is StringContainer s)
                {
                    return method.Evaluate(s);
                }
                throw new STRException($"unknown token type {identityValue.GetType()}");
            }
        }
    }
}
