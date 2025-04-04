import { useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "../styles/RegisterForm.module.css";
import SocialSignIn from "./SocialSignIn";

const RegisterForm = ({ onClose, toggleAuth }) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();
    
    if (password !== confirmPassword) {
      alert("Пароли не совпадают!");
      return;
    }

    // Сохранение пользователя в локальном хранилище
    localStorage.setItem("user", JSON.stringify({ username: email, isNew: true }));

    // Закрытие модалки
    onClose();

    // Перенаправление в профиль
    navigate("/profile");
  };

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <h2>Регистрация</h2>
      <input 
        type="email" 
        placeholder="Email" 
        value={email} 
        onChange={(e) => setEmail(e.target.value)} 
        required 
      />
      <input 
        type="password" 
        placeholder="Пароль" 
        value={password} 
        onChange={(e) => setPassword(e.target.value)} 
        required 
      />
      <input 
        type="password" 
        placeholder="Подтвердите пароль" 
        value={confirmPassword} 
        onChange={(e) => setConfirmPassword(e.target.value)} 
        required 
      />
      <button type="submit">Зарегистрироваться</button>

      <div className={styles.orDivider}>или</div>
      <SocialSignIn />

      <p className={styles.loginLink}>
        Уже есть аккаунт? <span onClick={toggleAuth}>Войти</span>
      </p>
    </form>
  );
};

export default RegisterForm;
