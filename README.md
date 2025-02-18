# PixlParkTestTask

## Сделать регистрацию пользователя по адресу эл. почты.

1. Клиент вводит адрес эл. почты, нажимает продолжить
2. Появляется поле, куда надо ввести код, который был сгенерирован системой и отправлен на введенную почту
3. Если код верный -- показать страницу что всё хорошо. Иначе -- выдать сообщение об ошибке.
   
## Технические требования
Web-приложение
Реализация на ASP.NET ( ASP.NET Core)
Сгенерированное письмо уходит или в очередь сообщений (RabbitMQ) или в базу данных.

### Сервер отправки писем
Консольное приложение на .NET (.NET Core)
Слушает очередь (или периодически опрашивает базу данных) и получив задание для отправки -- выводит его в консоль (например, 2023.04.10 18:30 test@example.com код: 1234 )
### Примечания
* Реализация через очереди будет бо́льшим плюсом, чем через БД.
* Локализуемость фраз интерфейса будет плюсом (en/ru)
* Защита от дудоса будет плюсом (таймеры на отправку, валидация, капча)
