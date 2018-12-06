#include <stdio.h>
#include <ctype.h>

#define LENGTH 16

void hd(FILE *in, FILE *out);
void print(FILE * out, long addr, unsigned char buffer[], int cnt);

int main(int argc, const char *argv[])
{
    int ret = 0;
    if(argc < 2)
    {
        hd(stdin, stdout);
    }
    else
    {
        int i;
        for(i = 1; i < argc; i++)
        {
            FILE *fp = fopen(argv[i], "r");
            if(NULL == fp)
            {
                fprintf(stderr, "Can't open file: %s\n", argv[i]);       
                ret++;     
            }
            else
            {
                hd(fp, stdout);

                fclose(fp);
            }
        }
    }

    return ret;
}

void hd(FILE *in, FILE *out)
{
    int cnt = 0;
    unsigned char buffer[LENGTH];
    long addr = 0L;

    int ch;
    //Beolvas a file-ból 1 char-t
    while(EOF != (ch = fgetc(in)))
    {
        buffer[cnt++] = ch;

        if(LENGTH == cnt)
        {
            print(out, addr, buffer, LENGTH);
            cnt = 0;
            addr += LENGTH;
        }
    }
    print(out, addr, buffer, cnt);
}

void print(FILE *out, long addr, unsigned char buffer[], int cnt)
{
    fprintf(out, "%08ld |", addr);

    int i;
    for(i = 0; i < cnt; i++)
    {
        fprintf(out, " %02x", buffer[i]);
    }
    for(; i < LENGTH; i++)
    {
        fprintf(out, "   ");
    }

    fprintf(out, " | ");

    for(i = 0; i < cnt; i++)
    {
        fprintf(out, "%c", isgraph(buffer[i]) ? buffer[i] : '.');
    }
    for(; i < LENGTH; i++)
    {
        fprintf(out, " ");
    }

    fprintf(out, " |\n");
}