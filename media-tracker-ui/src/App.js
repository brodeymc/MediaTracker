import { useState } from "react";
import API from "./api";

function App() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [token, setToken] = useState("");

    const login = async () => {
        const res = await API.post("/auth/login", {
            username,
            password
        });

        setToken(res.data.token);
    };

    return (
        <div>
            <h1>Media Tracker</h1>
            <input placeholder="Username" onChange={e => setUsername(e.target.value)} />
            <input placeholder="Password" type = "password" onChange={e => setPassword(e.target.value)} />

            <button onClick={login}>Login</button>
        </div>
    )
}



export default App;
