#pragma once

class Jarmu;

class Terep {
    public:
        virtual void halad(Jarmu* p) = 0;
};

class Homok: public Terep {
    public:
        static Homok* peldany();
        void halad(Jarmu* p) override ;
    protected:
        Homok(){}
    private:
        static Homok* _peldany;
};

class Domb: public Terep {
    public:
        static Domb* peldany();
        void halad(Jarmu* p) override ;
    protected:
        Domb(){}
    private:
        static Domb* _peldany;
    };

class Viz: public Terep {
    public:
        static Viz* peldany();
        void halad(Jarmu* p) override ;
    protected:
        Viz(){}
    private:
        static Viz* _peldany;
};


