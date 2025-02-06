import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../utils/auth";
import styles from "../styles/SignInForm.module.css";
import SocialSignIn from "./SocialSignIn";

const SignInForm = ({ onClose }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();
    if (login(username, password)) {
      navigate("/profile"); // ✅ Переход в профиль после входа
      onClose();
    } else {
      alert("❌ Неверные данные! Попробуйте user / user");
    }
  };

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <h2>Войти</h2>
      <input type="text" placeholder="Логин" value={username} onChange={(e) => setUsername(e.target.value)} required />
      <input type="password" placeholder="Пароль" value={password} onChange={(e) => setPassword(e.target.value)} required />
      <button type="submit" className={styles.submitBtn}>Войти</button>

      <div className={styles.orDivider}>или</div>
      <SocialSignIn />

      <p className={styles.registerLink}>
        Нет аккаунта? <span onClick={() => navigate("/register")}>Зарегистрируйтесь</span>
      </p>
    </form>
  );
};

export default SignInForm;
