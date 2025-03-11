import { useState } from "react";
import styles from "../styles/SearchBar.module.css";

const continents = [
  "Северная Америка",
  "Южная Америка",
  "Европа",
  "Азия",
  "Африка",
  "Австралия",
  "Антарктида",
];

const SearchBar = ({ onFocus, onBlur }) => {
  const [isLocationOpen, setIsLocationOpen] = useState(false);
  const [isDateOpen, setIsDateOpen] = useState(false);
  const [isDateOutOpen, setIsDateOutOpen] = useState(false);
  const [location, setLocation] = useState("");
  const [checkIn, setCheckIn] = useState("");
  const [checkOut, setCheckOut] = useState("");
  const [guests, setGuests] = useState(1);

  return (
    <div className={styles.searchContainer} onFocus={onFocus} onBlur={onBlur}>
      <div className={styles.searchBar}>
        {/* Локация */}
        <div className={styles.searchField}>
          <input
            type="text"
            placeholder="Куда"
            value={location}
            onClick={() => setIsLocationOpen(!isLocationOpen)}
            readOnly
          />
          {isLocationOpen && (
            <div className={styles.dropdown}>
              {continents.map((continent) => (
                <div
                  key={continent}
                  className={styles.option}
                  onClick={() => {
                    setLocation(continent);
                    setIsLocationOpen(false);
                  }}
                >
                  {continent}
                </div>
              ))}
            </div>
          )}
        </div>

        {/* Разделитель */}
        <div className={styles.divider} />

        {/* Дата прибытия */}
        <div className={styles.searchField}>
          <input
            type="text"
            placeholder="Прибытие"
            value={checkIn}
            onFocus={() => setIsDateOpen(true)}
            readOnly
          />
          {isDateOpen && (
            <div className={styles.calendar}>
              <input
                type="date"
                onChange={(e) => {
                  setCheckIn(e.target.value);
                  setIsDateOpen(false);
                }}
              />
            </div>
          )}
        </div>

        {/* Разделитель */}
        <div className={styles.divider} />

        {/* Дата отбытия */}
        <div className={styles.searchField}>
          <input
            type="text"
            placeholder="Отбытие"
            value={checkOut}
            onFocus={() => setIsDateOutOpen(true)}
            readOnly
          />
          {isDateOutOpen && (
            <div className={styles.calendar}>
              <input
                type="date"
                onChange={(e) => {
                  setCheckOut(e.target.value);
                  setIsDateOutOpen(false);
                }}
              />
            </div>
          )}
        </div>

        {/* Разделитель */}
        <div className={styles.divider} />

        {/* Гости */}
        <select value={guests} onChange={(e) => setGuests(e.target.value)}>
          {[...Array(10).keys()].map((num) => (
            <option key={num + 1} value={num + 1}>
              {num + 1} {num === 0 ? "гость" : "гостей"}
            </option>
          ))}
        </select>

        {/* Кнопка поиска */}
        <button type="submit">🔍</button>
      </div>
    </div>
  );
};

export default SearchBar;
