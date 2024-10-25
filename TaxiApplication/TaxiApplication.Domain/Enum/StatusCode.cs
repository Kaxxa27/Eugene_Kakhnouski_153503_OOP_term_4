using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApplication.Domain.Enum;

public enum StatusCode
{
	/// <summary>
	/// User StatusCode 1**-> 
	/// 0-1*   -> 0 -> UserNotFound
	///			  1 -> UserAlreadyExist
	/// 0-1**  ->
	///  
	/// </summary>
	UserNotFound = 100,
	UserAlredyExist = 110,
	
	AllError = 404,
	OK = 777,

}
