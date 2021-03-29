using System.Collections.Generic;
using System.ServiceModel;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{
	[OperationContract]
	bool MakeLogin(string userName, string password);

	[OperationContract]
	List<string> GetAllUsers();

	[OperationContract]
	int DeleteUsers(List<string> usersToDelete);
}

