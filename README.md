هدف برنامه ثبت جزئیات حقوق دریافتی هرماه و نحوه محاسبه اضافه کار و حقوق دریافتی هر کارمند از طریق api می باشد 
ذخیره ،به روز رسانی و حذف اطلاعات از طریق   orm entityframework و واکشی اطلاعات از طریق dapper انجام شده است  

در این پروژه از معماری clean architecture و همچنین از asp.net 6  و برای unti test از xunit استفاده شده است 
برای مستند سازی api نیز  swagger و از sql server نیز برای دیتابیس استفاده شده است  
پروژه داکرایز شده و برای بیلد پروژه و اماده کردن ایمیج مربوطه میتوانید از کامند زیر استفاده کنید  
docker build -f "Src\CalculationOfEmployeeSalaries.Application\Dockerfile" --force-rm -t calculationofemployeesalariesapplication "Src"


در اینده قصد دارم پروژه را ارتقا داده و برای دیتابیس read از nosql استفاده کنم و cqrs و همچنین متدهای تست را کاملتر کنم 
