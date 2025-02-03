import { useState } from "react";
import styles from "../styles/RegisterForm.module.css";
import SocialSignIn from "./SocialSignIn"; 

const RegisterForm = ({ onSubmit, onToggleLogin }) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    if (password !== confirmPassword) {
      alert("Пароли не совпадают!");
      return;
    }
    onSubmit({ email, password });
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

      <SocialSignIn /> {/* google, facebook, apple */}

      <p className={styles.loginLink}>
        Уже есть аккаунт? <span onClick={onToggleLogin}>Войти</span>
      </p>
    </form>
  );
};

export default RegisterForm;
