import styles from "./SocialSignIn.module.css";

const SocialSignIn = () => {
  const handleSocialSignIn = (provider) => {
    console.log(`Вход через ${provider}`);
    alert(`Вход через ${provider} в разработке 🚧`);
  };

  return (
    <div className={styles.socialButtons}>
      <button onClick={() => handleSocialSignIn("Google")} className={styles.google}>
        <img src="/icons/google.svg" alt="Google" />
        Войти через Google
      </button>
      <button onClick={() => handleSocialSignIn("Facebook")} className={styles.facebook}>
        <img src="/icons/facebook.svg" alt="Facebook" />
        Войти через Facebook
      </button>
      <button onClick={() => handleSocialSignIn("Apple")} className={styles.apple}>
        <img src="/icons/apple.svg" alt="Apple" />
        Войти через Apple
      </button>
    </div>
  );
};

export default SocialSignIn;
