import { useState } from "react";
import { setToken as saveToken } from "./api";
import API from "./api";

function App() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [items, setItems] = useState([]);
    const [title, setTitle] = useState("");

    const fetchItems = async () => {
        const res = await API.get("/media");
        setItems(res.data);
    }

    const createItem = async () => {
        await API.post("/media",{
            title,
            mediaType: "Game",
            status: "InProgress"
        });

        fetchItems();
    }

    const login = async () => {
        const res = await API.post("/auth/login", {
            username,
            passwordHash: password
        });

        saveToken(res.data.token);
    };

    return (
        <div>
            <h1>Media Tracker</h1>
            <input placeholder="Username" onChange={e => setUsername(e.target.value)} />
            <input placeholder="Password" type = "password" onChange={e => setPassword(e.target.value)} />

            <button onClick={login}>Login</button>
            <button onClick={fetchItems}>Load Media</button>

            <ul>
                {items.map(item => (
                    <li key = {item.id}>{item.title}</li>
                ))}
            </ul>

            <input placeholder="Title" onChange={e => setTitle(e.target.value)}/>
            <button onClick={createItem}>Add</button>
        </div>
    )
}



export default App;
