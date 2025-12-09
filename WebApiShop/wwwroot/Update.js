
async function Update() {
    try {
        const email = document.querySelector("#userName").value;
        const firstName = document.querySelector("#firstName").value;
        const lastName = document.querySelector("#lastName").value;
        const password = document.querySelector("#password").value;
        q = await JSON.parse(localStorage.getItem("users"));

        const updateData = {
            Email: email,
            FirstName: firstName,
            LastName: lastName,
            Password: password,
        };
        //const strength = checkPasswordStrength();
        //if (strength < 2) {
        //    alert("הסיסמה חלשה מדי, נסה סיסמה חזקה יותר");
        //    return;
        //}

        const response = await fetch(`https://localhost:44378/api/users/${q.id}`,
            {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(updateData)
            });

        //if (!response.ok) {
        //    alert("עדכון נכשל, נסה שוב");
        //    throw new Error(`HTTP error! status${response.status}`)
        //}

        if (response.status == 400) {
            alert("העדכון נכשל, הסיסמא לא חזקה מספיק")
        }

        if (response.ok) {
            alert("עודכן בהצלחה");
        }


    }
    catch (e) {
        console.log(e);
    }
    
}

