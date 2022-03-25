const langselect =
{
    "Register": {
        "ru": "Регистрация",
        "en": "Register",
    },
    
    "Login": {
        "ru": "Войти",
        "en": "Login",
    },
    "Find": {
        "ru": "Найти",
        "en": "Find",
    },
    
    "Logout": {
        "ru": "Выйти",
        "en": "Logout",
    },
    "Profile": {
        "ru": "Профиль",
        "en": "Profile",
    },
    "Create": {
        "ru": "Создать",
        "en": "Сreate",
    },
    "Firstname": {
        "ru": "Имя",
        "en": "Firstname",
    },
    "Lastname": {
        "ru": "Фамилия",
        "en": "Lastname",
    },
    "Username": {
        "ru": "Имя пользователя",
        "en": "Username",
    },
    "Password": {
        "ru": "Пароль",
        "en": "Password",
    },
    "Usernamee": {
        "ru": "Имя пользователя",
        "en": "Username",
    },
    "Passworde": {
        "ru": "Пароль",
        "en": "Password",
    },
        "haveacc": {
            "ru": "У Вас уже есть учетная запись?",
            "en": "Already have an account?",
        },
        "notacc": {
            "ru": "У Вас еще нет учетной записи?",
            "en": "Don't have an account yet?",
        },

};

const select = document.getElementById("lang-select");
const lang = ['en', 'ru'];
select.addEventListener('change', changeURLLang);

function changeURLLang() {
   // localStorage.setItem('lang', lang);
   
    let lang = select.value;
    location.href = window.location.pathname + '#' + lang;
    changeLang();
   // location.reload();
}

function changeLang() {
    let hash = window.location.hash;
    hash = hash.substr(1);
    localStorage.setItem('en', hash);
   hash= localStorage.getItem('en');
    console.log(hash);
    //if (!lang.includes(hash)) {
    //    location.href = window.location.pathname + '#en';
    //    console.log(hash);
    //}
    

   
    
    for (key in langselect) {

        
            
        if (document.getElementById('lang_' + key)) {
            document.getElementById('lang_' + key).innerHTML = langselect[key][hash];
            console.log('.lang_' + key);
        }
        //else {
        //    console.log(document.getElementById('lang_' + key).placeholder));
        //    document.getElementById('lang_' + key).placeholder = langselect[key][hash];
        //}
       
    }
}
