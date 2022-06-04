Использованные Nuget's - IronXl (для чтения даннных с Excel), SeleniumWebDriver и WebDriverManager.

При нажатии кнопки "открыть файл" показывается, как будет выглядить Excel файл.
При нажатии на кнопку браузера и выбора файла откроется браузер, в котором будет заполняться формула.
Для успешного выполнения [челленджа](https://rpachallenge.com/) нужно скачать Excel файл с сайта, выбрать один из трех браузеров в программе, после чего указать путь к скачанному файлу. После этого запускается выбранный браузер, который сам начнет и выполнит челлендж)

Проблемы и узкие горла:
1. Не используется архитектура MVVM, из-за возникают сложности с хранением и выведением данных. Именно из-за этого даже если нажать "открыть файл" и загрузить таблицу, то потом при нажатии на кнопку браузера файл придется загружать еще раз (и экземпляр класса будет инициализирован еще раз)
2. Из-за разных драйверов под браузера код "Selenium.cs" был продублирован под каждый браузер. Поэтому комментарии к коду содержаться только в одном экземляре - SeleniumFirefox.css.
3. Возникли проблемы с IronXl - постоянно появлялась пустая строка и не было всех данных, поэтому данные с Excel сначала обрабатываются, а потом выводятся (при реализации с MVVM такой проблемы скорее всего не было).
4. Возможно, можно еще ускороить ввод данных слеудющим образом:
    1. Для каждого раунда (их всего 10) клонировать массив с тегами инпутов
    2. Когда находим для инпута нужный тег - удаляем его из массива - остальным инпутам не придется проходить проверку с заведо невозможным тегом
    3. Если найден инпут, ассоциируемый с "Sumbit" - меняем его с инпутом на последнем месте и уменьшаем индекс на 1
    4. На последнем шаге, когда инпут всегда явлется кнопеой "Sumbit" - нажимаем её.
 
Пример выполнения RPA Challenge:![kHBdIifh6Zg-Uwfqvoiz4yUjOaHzlQ9sWXXv9W94SPWbbRCddnDQkatJ8j_lyGObDoB7AUHEBAAwFDuMeAXoKddw](https://user-images.githubusercontent.com/83097203/171958253-caa6980f-dfa5-4729-8423-3cbdd34cfb4a.jpg)
