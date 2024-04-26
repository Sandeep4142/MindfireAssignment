import * as React from "react";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import { AppBar, ThemeProvider } from "@mui/material";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import MenuIcon from "@mui/icons-material/Menu";
import Container from "@mui/material/Container";
import Button from "@mui/material/Button";
import MenuItem from "@mui/material/MenuItem";
import LocalHospitalOutlinedIcon from "@mui/icons-material/LocalHospitalOutlined";
import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    primary: {
      main: "#311b92",
    },
  },
});

function Header({ isSignedIn, setIsSignedIn }) {
  const [anchorElNav, setAnchorElNav] = React.useState(null);
  const navigateTo = useNavigate();

  const handleOpenNavMenu = (event) => {
    setAnchorElNav(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  const logOut = () => {
    setIsSignedIn((prev) => !prev);
    localStorage.clear();
    navigateTo("/SignIn");
  };

  return (
    <ThemeProvider theme={theme}>
      <AppBar
        position="static"
        sx={{
          backgroundColor: "primary.main",
          position: "fixed",
          zIndex: 5,
          top: 0,
        }}
      >
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
                    <MenuItem key="patient" onClick={handleCloseNavMenu}>
                      <Link
                        to="/Patient"
                        variant="body2"
                        style={{
                          color: "black",
                          marginLeft: "10px",
                          textDecoration: "none",
                        }}
                      >
                        Patients
                      </Link>
                    </MenuItem>
                    <MenuItem key="appointment" onClick={handleCloseNavMenu}>
                      <Link
                        to="/Appointment"
                        variant="body2"
                        style={{
                          color: "black",
                          marginLeft: "10px",
                          textDecoration: "none",
                        }}
                      >
                        Appointment
                      </Link>
                    </MenuItem>
                  </Menu>
                </Box>
                <LocalHospitalOutlinedIcon
                  sx={{ display: { xs: "flex", md: "none" }, mr: 1 }}
                />
                <Typography
                  variant="body1"
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
                    style={{
                      color: "white",
                      marginLeft: "20px",
                      textDecoration: "none",
                      fontSize: "19px",
                    }}
                  >
                    Patients
                  </Link>
                  <Link
                    to="/Appointment"
                    variant="body1"
                    style={{
                      color: "white",
                      marginLeft: "20px",
                      textDecoration: "none",
                      fontSize: "19px",
                    }}
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
    </ThemeProvider>
  );
}
export default Header;
