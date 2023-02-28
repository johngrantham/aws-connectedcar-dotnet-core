using System.Collections.Generic;

namespace ConnectedCar.Core.Shared.AuthPolicy
{
    public class AuthPolicyFactory
    {
        public static AuthPolicy GetApiPolicy(string principalId, bool isAllowed)
        {
            AuthPolicy policy = new AuthPolicy
            {
                PrincipalId = principalId,
                PolicyDocument = new AuthPolicyDocument
                {
                    Version = "2012-10-17",
                    Statement = new List<AuthPolicyStatement>()
                }
            };

            policy.PolicyDocument.Statement.Add(new AuthPolicyStatement
            {
                Action = "execute-api:Invoke",
                Effect = isAllowed ? "Allow" : "Deny",
                Resource = "*"
            });

            return policy;
        }
    }
}