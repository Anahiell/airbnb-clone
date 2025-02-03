import { useState } from "react";
import styles from "../styles/SignInForm.module.css";

const SignInForm = ({ onSubmit, onToggleRegister }) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit({ email, password });
  };

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <h2>Войти</h2>

      <div className={styles.inputGroup}>
        <label>Email</label>
        <input 
          type="email" 
          value={email} 
          onChange={(e) => setEmail(e.target.value)} 
          required 
        />
      </div>

      <div className={styles.inputGroup}>
        <label>Пароль</label>
        <input 
          type="password" 
          value={password} 
          onChange={(e) => setPassword(e.target.value)} 
          required 
        />
      </div>

      <button type="submit" className={styles.submitBtn}>Войти</button>

      <p className={styles.registerLink}>
        Нет аккаунта? <span onClick={onToggleRegister}>Зарегистрируйтесь</span>
      </p>
    </form>
  );
};

export default SignInForm;
