import { extendTheme, ThemeConfig, theme as base } from '@chakra-ui/react';

const config: ThemeConfig = {
  initialColorMode: 'light',
  useSystemColorMode: true,
  cssVarPrefix: 'jimbo',
};

const theme = extendTheme({
  config,
  styles: {
    global: {
      body: {
        fontFeatureSettings: '"ss02"',
      },
    },
  },
  fonts: {
    heading: `Inter, ${base.fonts.heading}`,
    body: `Inter, ${base.fonts.body}`,
  },
  components: {
    Input: {
      variants: {
        filled: {
          field: {
            _focus: {
              borderColor: 'purple.500',
              borderWidth: '2px',
            },
          },
        },
      },
    },
  },
});

export default theme;
