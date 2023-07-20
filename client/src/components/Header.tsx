import { Link } from "react-router-dom";

export function Header() {
  return (
    <header>
      <Link to={"/"} className="headerLinkLogo">My Blog</Link>
      <nav className="headerNav">
        <Link to={"/login"}>Login</Link>
        <Link to={"/register"}>Register</Link>
      </nav>
    </header>
  )
}