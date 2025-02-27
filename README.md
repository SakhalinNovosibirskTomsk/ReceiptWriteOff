# Микровервис для регистрации фактов прихода и списания книг.
В данном сервисе пользователь (администратор или библиотекарь) может оформить и удалить факты прихода и списания книг. В факте прихода фиксируется дата прихода и экземпляр книги. В факте списания фиксируется экземпляр книги, дата списания и причина списания.


## Сущности:
+ Пользователь
+ Экземпляр книги
+ Факт прихода
+ Факт списания
+ Причина списания


## Действия сущностей:
### Пользователь может:
- Создать/получить/отредактировать/удалить факт прихода книги
- Создать/получить/отредактировать/удалить факт списания книги


## Ограничения:
+ Нельзя зарегистрировать больше одного факта прихода и списания для одного экземпляра книги.
+ Для одного экземпляра книги нельзя, чтобы дата в факте списания была раньше, чем дата в факте прихода.
+ Инвентарные номера у экземпляров книг не должны совпадать.
+ Нельзя удалить факт прихода, если для того же экземпляра книги есть факт списания.


## Информация о сущностях:
### Пользователь
+ ID
+ ФИО

### Экземпляр книги
+ ID
+ Книга
+ Инвентарный номер

### Факт прихода
+ ID
+ Экземпляр книги
+ Дата
+ Пользователь

### Факт списания
+ ID
+ Экземпляр книги
+ Дата
+ Пользователь
+ Причина списания

### Причина списания
+ ID
+ Описание


## Межсервисное взаимодействие:
### Сервис Каталог может:
+ Добавить и удалить новую Книгу

### Сервис Аутентификации и Авторизации может:
+ Добавить и удалить пользователя с ролью Библиотекарь или Администратор


## Диаграмма вариантов использования сервиса ReceiptWriteOff

[![Диаграмма вариантов использования сервиса ReceiptWriteOff](https://github.com/SakhalinNovosibirskTomsk/ReceiptWriteOff/blob/main/Docs/Use%20cases%20%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81%D0%B0%20ReceiptWriteOff.png)](https://github.com/SakhalinNovosibirskTomsk/ReceiptWriteOff/blob/main/Docs/Use%20cases%20%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81%D0%B0%20ReceiptWriteOff.png)
[Ссылка на картинку](https://github.com/SakhalinNovosibirskTomsk/ReceiptWriteOff/blob/main/Docs/Use%20cases%20%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81%D0%B0%20ReceiptWriteOff.png)

[Ссылка на исходник схемы](https://github.com/SakhalinNovosibirskTomsk/ReceiptWriteOff/blob/main/Docs/Use%20cases%20%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81%D0%B0%20ReceiptWriteOff.drawio)


## БД сервиса ReceiptWriteOff

### ER-диаграмма
[![ER-диаграмма БД сервиса ReceiptWriteOff](https://github.com/SakhalinNovosibirskTomsk/ReceiptWriteOff/blob/main/Docs/ER-%D0%B4%D0%B8%D0%B0%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0%20%D0%91%D0%94%20%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81%D0%B0%20ReceiptWriteOff.drawio.png)](https://github.com/SakhalinNovosibirskTomsk/ReceiptWriteOff/blob/main/Docs/ER-%D0%B4%D0%B8%D0%B0%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0%20%D0%91%D0%94%20%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81%D0%B0%20ReceiptWriteOff.drawio.png)
[Ссылка на картинку](https://github.com/SakhalinNovosibirskTomsk/ReceiptWriteOff/blob/main/Docs/ER-%D0%B4%D0%B8%D0%B0%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0%20%D0%91%D0%94%20%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81%D0%B0%20ReceiptWriteOff.drawio.png)

[Ссылка на исходник схемы](https://github.com/SakhalinNovosibirskTomsk/ReceiptWriteOff/blob/main/Docs/ER-%D0%B4%D0%B8%D0%B0%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0%20%D0%91%D0%94%20%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81%D0%B0%20ReceiptWriteOff.drawio)
