import * as React from "react";
import { useNavigate } from "react-router-dom";
import { Link } from 'react-router-dom';
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import MenuIcon from "@mui/icons-material/Menu";
import Container from "@mui/material/Container";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import AdbIcon from "@mui/icons-material/Adb";
import LocalHospitalOutlinedIcon from "@mui/icons-material/LocalHospitalOutlined";

const pages = ["Patients", "Appointments"];
const settings = ["Profile", "Account", "Dashboard", "Logout"];

function Header({ isSignedIn, setIsSignedIn }) {
  const [anchorElNav, setAnchorElNav] = React.useState(null);
  const [anchorElUser, setAnchorElUser] = React.useState(null);
  const navigateTo = useNavigate();

  const handleOpenNavMenu = (event) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleOpenUserMenu = (event) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  const logOut = () => {
    setIsSignedIn((prev) => !prev);
    localStorage.clear();
    navigateTo("/SignIn");
  };

  return (
    <AppBar position="static">
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <LocalHospitalOutlinedIcon
            sx={{ display: { xs: "none", md: "flex" }, mr: 1 }}
          />
          <Typography
            variant="h6"
            noWrap
            component="a"
            href="#app-bar-with-responsive-menu"
            sx={{
              mr: 2,
              display: { xs: "none", md: "flex" },
              color: "inherit",
              textDecoration: "none",
            }}
          >
            Patient Record and Appointment Management
          </Typography>
          {isSignedIn ? (
            <>
              <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
                <IconButton
                  size="large"
                  aria-label="account of current user"
                  aria-controls="menu-appbar"
                  aria-haspopup="true"
                  onClick={handleOpenNavMenu}
                  color="inherit"
                >
                  <MenuIcon />
                </IconButton>
                <Menu
                  id="menu-appbar"
                  anchorEl={anchorElNav}
                  anchorOrigin={{
                    vertical: "bottom",
                    horizontal: "left",
                  }}
                  keepMounted
                  transformOrigin={{
                    vertical: "top",
                    horizontal: "left",
                  }}
                  open={Boolean(anchorElNav)}
                  onClose={handleCloseNavMenu}
                  sx={{
                    display: { xs: "block", md: "none" },
                  }}
                >
                  <MenuItem key="patient" onClick={handleCloseNavMenu} sx={{ fontSize: "18px", color: "white", marginLeft: '10px' }}>
                    <Link
                      to="/Patient"
                      variant="body2"
                      style={{ color: "white", marginLeft: '10px', textDecoration: "none", fontSize: "18px", }}
                    >
                      Patients
                    </Link>
                  </MenuItem>
                  <MenuItem key="appointment" onClick={handleCloseNavMenu} sx={{ fontSize: "18px", color: "white", marginLeft: '10px' }}>
                    <Link
                      to="/Appointment"
                      variant="body2"
                      style={{ color: "white", marginLeft: '10px', textDecoration: "none", fontSize: "18px", }}
                    >
                      Appointment
                    </Link>
                  </MenuItem>
                </Menu>
              </Box>
              <AdbIcon sx={{ display: { xs: "flex", md: "none" }, mr: 1 }} />
              <Typography
                variant="h5"
                noWrap
                component="a"
                href="#app-bar-with-responsive-menu"
                sx={{
                  mr: 2,
                  display: { xs: "flex", md: "none" },
                  flexGrow: 1,
                  color: "inherit",
                  textDecoration: "none",
                }}
              >
                Patient Record and Appointment Management Portal
              </Typography>
              <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
                <Link
                  to="/Patient"
                  variant="body1"
                  style ={{ color: "white", marginLeft: '20px', textDecoration: "none",fontSize: "19px", }}
                >
                  Patients
                </Link>
                <Link
                  to="/Appointment"
                  variant="body1"
                  style={{ color: "white", marginLeft: '20px', textDecoration: "none", fontSize: "19px", }}
                >
                  Appointments
                </Link>
              </Box>

              <Button
                variant="outlined"
                color="secondary"
                sx={{
                  color: "white",
                  borderColor: "white",
                  "&:hover": {
                    color: "black",
                    backgroundColor: "white",
                    borderColor: "white",
                  },
                }}
                onClick={() => {
                  logOut();
                }}
              >
                LogOut
              </Button>
            </>
          ) : (
            ""
          )}
        </Toolbar>
      </Container>
    </AppBar>
  );
}
export default Header;
