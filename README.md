هدف برنامه ثبت جزئیات حقوق دریافتی هرماه و نحوه محاسبه اضافه کار و حقوق دریافتی هر کارمند از طریق api می باشد 
ذخیره ،به روز رسانی و حذف اطلاعات با استفاده از EF core و واکشی اطلاعات از طریق dapper انجام شده است  

در این پروژه از معماری clean architecture و همچنین از asp.net core 6  و برای unit test از xunit استفاده شده است 
برای مستند سازی api نیز  swagger و از sql server نیز برای دیتابیس استفاده شده است  
پروژه داکرایز شده و برای بیلد پروژه و اماده کردن ایمیج مربوطه میتوانید از کامند زیر استفاده کنید  
docker build -f "Src\CalculationOfEmployeeSalaries.Application\Dockerfile" --force-rm -t calculationofemployeesalariesapplication "Src"


نمونه ورودی برای فرمت custom:
{ 
  "Data":
  "Line1:FirstName/LastName/NationalCode/BasicSalary/Allowance/Transportation/Date
  Line2: mehdi/Ahmadi/1223569875/1000000/300000/300000/14010801",

  "OverTimeCalculator": "CalculatorB"
}


در اینده قصد دارم پروژه را ارتقا داده و برای دیتابیس read از nosql استفاده کنم و cqrs و همچنین متدهای تست را کاملتر کنم 
