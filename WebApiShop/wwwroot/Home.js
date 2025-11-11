

async function getResponse() {
    try {
        const response = await fetch(
            'https://localhost:44378/api/users'
        );

        if (!response.ok) {
            throw new Error('error');
        }
        else {
            const data = await response.json();
            alert(data)
        }
    }
    catch (e) {
        console.log(e);
    }
}


async function newUser() {
    try {
        const user = document.querySelector("#userName");
        const first = document.querySelector("#firstName");
        const last = document.querySelector("#lastName");
        const pass = document.querySelector("#password");

        const postData = {
            Email: user.value,
            FirstName: first.value,
            LastName: last.value,
            Password: pass.value
        };
        const response = await fetch('https://localhost:44378/api/users',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(postData)
            });

        const dataPost = await response;
        if (response.ok)
            alert("משתמש נרשם בהצלחה!!!");
        else
            alert("הרישום לא הצליח");
    }
    catch (e) {
        console.log(e);
    }
}


async function Login() {
    try {
        const user = document.querySelector("#userNameLI");
        const pass = document.querySelector("#passwordLI");

        const dataLogin = {
            Email: user.value,
            FirstName: "",
            LastName: "",
            Password: pass.value
        };
        const response = await fetch('https://localhost:44378/api/users/Login',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(dataLogin)
            });

        if (!response.ok) {
            throw new Error(`HTTP error! status${response.status}`)
        }

        if (response.status == 204)
            alert("משתמש אינו רשום במערכת, הירשם כמשתמש חדש");

        else {
            const data = await response.json();
            console.log('POST Data:', await data);
            sessionStorage.setItem("users", JSON.stringify(await data));

            window.location.href = "../Update.html";
        }
    }
    catch (e) {
        console.log(e);
    }

}
